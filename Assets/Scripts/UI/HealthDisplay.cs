using UnityEngine;

/// <summary>
/// Handles the health display, responding when the entity is damaged by 
/// deactivating the right most health image. Invokes an event when the entity
/// runs out of health.
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] healthImages;
    [SerializeField] private GameEvent died;

    public void OnDamaged()
    {
        for(int i = healthImages.Length - 1; i >= 0; i--)
        {
            if (healthImages[i].activeInHierarchy)
            {
                healthImages[i].SetActive(false);
                return;
            }
        }

        died.Raise();
    }

    public void OnHealed()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (!healthImages[i].activeInHierarchy)
            {
                healthImages[i].SetActive(true);
                return;
            }
        }
    }
}
