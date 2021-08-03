using UnityEngine;

/// <summary>
/// Abstract class of effects that can be gained when the player collects 
/// pickups.
/// </summary>
public abstract class PickupEffect : ScriptableObject
{
    /// <summary>
    /// Bestows the pickup effect onto the player.
    /// </summary>
    /// <param name="player">Player to whom the effect is granted.</param>
    public abstract void Grant(Player player);
}
