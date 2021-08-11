using UnityEngine;

/// <summary>
/// Handles the health display, responding when the entity is damaged by 
/// deactivating the right most health image. Invokes an event when the entity
/// runs out of health.
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] healthImages;
    [SerializeField] private GameEvent playerDied;

    public void OnDamaged()
    {
        for(int i = healthImages.Length - 1; i >= 1; i--)
        {
            if (healthImages[i].activeInHierarchy)
            {
                healthImages[i].SetActive(false);
                return;
            }
        }

        healthImages[0].SetActive(false);
        playerDied.Raise();
    }

    public void OnGameReset()
    {
        foreach (GameObject healthImage in healthImages)
        {
            healthImage.SetActive(true);
        }
    }

    public void OnHealed()
    {
        foreach (GameObject healthImage in healthImages)
        {
            if (!healthImage.activeInHierarchy)
            {
                healthImage.SetActive(true);
                return;
            }
        }
    }
}
