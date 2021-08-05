using UnityEngine;

/// <summary>
/// Pickup effect that prevents the player from taking all damage for a 
/// specified time.
/// </summary>
[CreateAssetMenu(fileName = "new Speed Effect",
    menuName = "Pickup Effect.../Speed")]
public class SpeedEffect : PickupEffect
{
    [Tooltip("How much to change the game speed by?")]
    public float gameSpeedDelta;

    public override void Grant(EffectHandler effectHandler)
    {
        effectHandler.ChangeGameSpeed(gameSpeedDelta);
    }
}
