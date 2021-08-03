using GD.MinMaxSlider;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Tooltip("Object pooler for the pickup object to be spawned.")]
    [SerializeField] private MultiObjectPooler pickupObjectPooler;

    [Tooltip("Chance that a pickup is spawned in between each pillar.")]
    [SerializeField] private FloatVariable pickupSpawnChance;

    [Tooltip("Range for pickup spawn heights.")]
    [SerializeField, MinMaxSlider(-10, 10)]
    private Vector2 pickupSpawnHeightRange;

    private void Start()
    {
        pickupObjectPooler.InitializePool();
    }

    /// <summary>
    /// Determines if a pickup should be spawned based on pickupSpawnChance.
    /// </summary>
    public void OnSpawnPickup()
    {
        if(Random.value <= pickupSpawnChance.Value)
        {
            SpawnPickup();
        }
    }

    /// <summary>
    /// Spawns a pickup object from the object pool at a random height.
    /// </summary>
    private void SpawnPickup()
    {
        GameObject pickupObject = pickupObjectPooler.GetRandomObject();

        if(pickupObject != null)
        {
            float randomSpawnHeight = Random.Range(pickupSpawnHeightRange.x,
            pickupSpawnHeightRange.y);
            pickupObject.transform.position = new Vector3(transform.position.x,
                randomSpawnHeight, transform.position.z);

            pickupObject.SetActive(true);
        }
    }
}
