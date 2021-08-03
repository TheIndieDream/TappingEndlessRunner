/// <summary>
/// A command used to tell the player to float upward when executed.
/// </summary>
public class FloatCommand
{
    /// <summary>
    /// The current input from the player controller.
    /// </summary>
    public bool Input;

    /// <summary>
    /// Changes the player input to match input from the player controller.
    /// </summary>
    /// <param name="player">Player to execute this command.</param>
    public void Execute(Player player)
    {
        player.Input = Input;
    }
}
