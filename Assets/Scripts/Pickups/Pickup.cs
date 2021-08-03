using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupEffect Effect
    {
        get
        {
            return effect;
        }
    }

    [SerializeField] private PickupEffect effect;

    [Header("Appearance")]

    [Tooltip("Color variable determining the color of this pickup")]
    [SerializeField] private ColorVariable color;

    [Tooltip("Sprite Renderer representing the pickup.")]
    [SerializeField] private SpriteRenderer pickupSpriteRenderer;

    [Tooltip("Mesh Renderer representing the pickup's outline.")]
    [SerializeField] private MeshRenderer pickupOutlineMeshRenderer;

    private void Update()
    {
        if(pickupSpriteRenderer.color != color.Value)
        {
            pickupSpriteRenderer.color = color.Value;
        }

        if(pickupOutlineMeshRenderer.material.GetColor("_BaseColor") != 
            color.Value)
        {
            pickupOutlineMeshRenderer.material.SetColor("_BaseColor",
                color.Value);
        }

        if(pickupOutlineMeshRenderer.material.GetColor("_EmissionColor") != 
            color.Value)
        {
            pickupOutlineMeshRenderer.material.SetColor("_EmissionColor",
                color.Value);
        }
    }
}
