using UnityEngine;

/// <summary>
/// Pickup effect that prevents the player from taking all damage for a 
/// specified time.
/// </summary>
[CreateAssetMenu (fileName = "new Invulnerable Effect", 
    menuName = "Pickup Effect.../Invulnerable")]
public class ShieldEffect : PickupEffect
{
    [Tooltip("How long should the player be immune to damage for?")]
    public float time;

    public override void Grant(Player player)
    {
        player.BecomeShielded(time);
    }
}
