using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Stream from which the player will execute fly commands.")]
    [SerializeField] private FloatCommandStream commandStream;

    private FloatCommand flyCommand;

    private void Start()
    {
        flyCommand = new FloatCommand();
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
            flyCommand.Input = true;
            commandStream.Enqueue(flyCommand);
        }
        if (context.canceled)
        {
            flyCommand.Input = false;
            commandStream.Enqueue(flyCommand);
        }
    }
}
