using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private string keyForVolumeSave;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = 1.0f;
    }

    public void OnVolumeChange(float newVolumePercent)
    {
        mainMixer.SetFloat(keyForVolumeSave, Mathf.Log(newVolumePercent) * 20);
        //PlayerPrefs.SetFloat(keyForVolumeSave, newVolumePercent);
    }
}
