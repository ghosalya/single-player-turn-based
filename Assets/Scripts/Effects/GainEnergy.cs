using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/GainEnergy")]
public class GainEnergy : Effect
{
    public int energyGained;
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.gainEnergy(energyGained);
    }
}
