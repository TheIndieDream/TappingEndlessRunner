using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// The current player input. When true, the player will float upward.
    /// </summary>
    public bool Input { get; set; }

    [SerializeField] private EffectHandler effectHandler;

    [Header("Movement")]

    [Tooltip("Force at which the player will float upward.")]
    [SerializeField] private float upwardForce;

    [Header("Control")]

    [Tooltip("Stream from which the player will execute float commands.")]
    [SerializeField] private FloatCommandStream commandStream;

    [Header("Game Events")]

    [Tooltip("GameEvent to signal the player has lost a health point.")]
    [SerializeField] private GameEvent damaged;

    [Tooltip("GameEvent to signal the player has lost all its health.")]
    [SerializeField] private GameEvent died;

    [Header("Color")]

    [Tooltip("Color of the player's outline.")]
    [SerializeField] private ColorVariable outlineColor;

    [Tooltip("Outline mesh renderer of the player.")]
    [SerializeField] private MeshRenderer outlineRenderer;

    /// <summary>
    /// Rigidbody2D component used for character movement.
    /// </summary>
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateColor();
    }

    private void FixedUpdate()
    {
        GetCommandFromStream();
        Float();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!effectHandler.Shield.IsActive)
        {
            died.Raise();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (!effectHandler.Shield.IsActive)
            {
                damaged.Raise();
            }
        }
        else if (collision.CompareTag("Pickup"))
        {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            pickup.Effect.Grant(effectHandler);
            pickup.OnPickUp();
        }
    }

    /// <summary>
    /// Adds upwardForce to the player's Rigidbody2D if there is player input.
    /// </summary>
    private void Float()
    {
        if (Input)
        {
            rb2d.AddForce(Vector2.up * upwardForce);
        }
    }

    /// <summary>
    /// Dequeues a command from the command stream and executes it if one is 
    /// available.
    /// </summary>
    private void GetCommandFromStream()
    {
        if (commandStream.Count() > 0)
        {
            commandStream.Dequeue().Execute(this);
        }
    }

    /// <summary>
    /// Changes the color of the player's outline to the value specified in
    /// outlineColor.
    /// </summary>
    private void UpdateColor()
    {
        outlineRenderer.material.SetColor("_BaseColor", outlineColor.Value);
        outlineRenderer.material.SetColor("_EmissionColor", outlineColor.Value);
    }
}
