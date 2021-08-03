using UnityEngine;

/// <summary>
/// Handles the player health display, responding when the player is damaged 
/// by deactivating the right most player health image. Invokes a player died 
/// event when the player runs out of health.
/// </summary>
public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] playerHealthImages;
    [SerializeField] private GameEvent playerDiedEvent;

    public void OnPlayerDamaged()
    {
        for(int i = playerHealthImages.Length - 1; i >= 0; i--)
        {
            if (playerHealthImages[i].activeInHierarchy)
            {
                playerHealthImages[i].SetActive(false);
                return;
            }
        }

        playerDiedEvent.Raise();
    }

    public void OnPlayerHealed()
    {
        for (int i = 0; i < playerHealthImages.Length; i++)
        {
            if (!playerHealthImages[i].activeInHierarchy)
            {
                playerHealthImages[i].SetActive(true);
                return;
            }
        }
    }
}
