using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buff/DamageBonus")]
public class DamageBonusBuff : Buff
{
    public int damageBonus = 0;
    public float damageMultiplier = 1;
    public override int AddBonusDamage(int damage) {
        return damage + damageBonus;
    }

    public override int MultiplyDamage(int damage) {
        return Mathf.RoundToInt(damageMultiplier * damage);
    }
}
