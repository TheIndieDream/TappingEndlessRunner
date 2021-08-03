using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Scroller : MonoBehaviour
{
    [SerializeField] private FloatVariable gameSpeed;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb2d.velocity = new Vector2(-gameSpeed.Value, 0.0f);
    }

    private void Update()
    {
        if(rb2d.velocity.x != -gameSpeed.Value)
        {
            rb2d.velocity = new Vector2(-gameSpeed.Value, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deactivator"))
        {
            gameObject.SetActive(false);
        }
    }
}
