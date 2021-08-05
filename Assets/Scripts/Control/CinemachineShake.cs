using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float playerDamagedShakeDuration;
    [SerializeField] private float playerDamagedShakeIntensity;
    [SerializeField] private float playerPickupShakeDuration;
    [SerializeField] private float playerPickupShakeIntensity;

    private CinemachineBasicMultiChannelPerlin cameraNoiseComponent;

    private void Awake()
    {
        cameraNoiseComponent = 
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void PlayerDamagedShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(playerDamagedShakeDuration,
            playerDamagedShakeIntensity));
    }

    public void PlayerPickupShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(playerPickupShakeDuration,
            playerPickupShakeIntensity));
    }

    private IEnumerator ShakeRoutine(float shakeDuration, float shakeIntensity)
    {
        float elapsedTime = 0;
        cameraNoiseComponent.m_AmplitudeGain = shakeIntensity;

        while (elapsedTime < shakeDuration)
        {
            cameraNoiseComponent.m_AmplitudeGain = 
                Mathf.Lerp(shakeIntensity, 0.0f, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraNoiseComponent.m_AmplitudeGain = 0.0f;
    }

}
