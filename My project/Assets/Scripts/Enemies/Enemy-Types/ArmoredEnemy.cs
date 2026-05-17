using UnityEngine;

public class ArmoredEnemy : Enemy
{
    [Header("Armor")]
    public float armorReduction = 0.5f;

    protected override void OnAwake()
    {
        moveSpeed = 5f;
        baseHealth = 200f;
    }


    protected override float ModifyIncomingDamage(float damage)
{
    return damage * armorReduction; 
}
}