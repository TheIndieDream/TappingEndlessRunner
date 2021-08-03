using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to hold a queue of commands for future execution by the 
/// player. Should be populated by some player controller class and commands
/// should be executed by some player class.
/// </summary>
[CreateAssetMenu]
public class FloatCommandStream : ScriptableObject
{
    /// <summary>
    /// Queue to store fly commands for future execution by the player.
    /// </summary>
    private Queue<FloatCommand> stream = new Queue<FloatCommand>();

    /// <summary>
    /// Adds a fly command to the stream for future execution by the player.
    /// </summary>
    /// <param name="command">Command to be enqueued.</param>
    public void Enqueue(FloatCommand command)
    {
        stream.Enqueue(command);
    }

    /// <summary>
    /// Retrieves a fly command from the stream for execution by the player.
    /// </summary>
    /// <returns>{FlyCommand} First FlyCommand in the stream.</returns>
    public FloatCommand Dequeue()
    {
        return stream.Dequeue();
    }

    /// <summary>
    /// Returns the current count of commands in the stream.
    /// </summary>
    /// <returns>{int} Current count of commands in the stream.</returns>
    public int Count()
    {
        return stream.Count;
    }
}
