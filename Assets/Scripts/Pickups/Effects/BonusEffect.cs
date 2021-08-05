using UnityEngine;

/// <summary>
/// Pickup effect that restores a set number of health points to the player.
/// </summary>
[CreateAssetMenu(fileName = "new Bonus Effect",
    menuName = "Pickup Effect.../Bonus")]
public class BonusEffect : PickupEffect
{
    [Tooltip("How many bonus points should be awarded to the player?")]
    public int number;

    public override void Grant(EffectHandler effectHandler)
    {
        effectHandler.AddBonus(number);
    }
}
