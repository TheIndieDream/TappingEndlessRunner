using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Stream from which the player will execute fly commands.")]
    [SerializeField] private FlyCommandStream commandStream;

    private FlyCommand flyCommand;

    private void Start()
    {
        flyCommand = new FlyCommand();
    }

    /// <summary>
    /// Response to "Float" input from the New Unity Input System. Enqueues a 
    /// fly command to the command stream.
    /// </summary>
    /// <param name="context">Input action context.</param>
    public void OnFloat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            commandStream.Enqueue(flyCommand);
        }
    }
}
