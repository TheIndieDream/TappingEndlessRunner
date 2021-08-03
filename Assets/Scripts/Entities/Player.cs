using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// The current player input. When true, the player will float upward.
    /// </summary>
    public bool Input { get; set; }

    [Header("Movement")]

    [Tooltip("Force at which the player will float upward.")]
    [SerializeField] private float upwardForce;

    [Tooltip("The speed at which the game is moving, and thus the apparent" +
        "speed of the player.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Header("Control")]

    [Tooltip("Stream from which the player will execute float commands.")]
    [SerializeField] private FloatCommandStream commandStream;

    [Header("Game Events")]

    [Tooltip("GameEvent to signal the player has lost a health point.")]
    [SerializeField] private GameEvent playerDamagedEvent;

    [Tooltip("GameEvent to signal the player has gained a health point.")]
    [SerializeField] private GameEvent playerHealedEvent;

    [Tooltip("GameEvent to signal the player has lost all its health.")]
    [SerializeField] private GameEvent playerDiedEvent;

    [Header("Color")]

    [Tooltip("Color of the player's outline.")]
    [SerializeField] private ColorVariable outlineColor;

    [Tooltip("Outline mesh renderer of the player.")]
    [SerializeField] private MeshRenderer outlineRenderer;

    /// <summary>
    /// Rigidbody2D component used for character movement.
    /// </summary>
    private Rigidbody2D rb2d;

    /// <summary>
    /// Determines if the player can take damage and die.
    /// </summary>
    private bool isShielded = false;

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
        if (!isShielded)
        {
            playerDiedEvent.Raise();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (!isShielded)
            {
                playerDamagedEvent.Raise();
            }
        }
        else if (collision.CompareTag("Pickup"))
        {
            GameObject pickupObject = collision.gameObject;
            pickupObject.GetComponent<Pickup>().Effect.Grant(this);
            pickupObject.SetActive(false);
        }
    }

    /// <summary>
    /// Makes the player invulnerable. Then, starts a coroutine to end the 
    /// invulnerability for after the passed time.
    /// </summary>
    /// <param name="time">Amount of time the effect will last.</param>
    public void BecomeShielded(float time)
    {
        isShielded = true;
        StopAllCoroutines();
        StartCoroutine(LoseShield(time));
    }

    /// <summary>
    /// Raises the playerHealedEvent.
    /// </summary>
    /// <param name="amount">Number of times to raise the event.</param>
    public void Heal(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            playerHealedEvent.Raise();
        }
    }

    /// <summary>
    /// Changes the speed at which objects move towards the player.
    /// </summary>
    /// <param name="amount">Amount by which to change the game speed.</param>
    public void ChangeGameSpeed(float amount)
    {
        if(gameSpeed.Value + amount > 0)
        {
            gameSpeed.Value += amount;
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
    /// Routine to make the player vulnerable after a specified amount of time.
    /// </summary>
    /// <param name="time">Time after which the player becomes 
    /// vulnerable.</param>
    private IEnumerator LoseShield(float time)
    {
        yield return new WaitForSeconds(time);
        isShielded = false;
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
