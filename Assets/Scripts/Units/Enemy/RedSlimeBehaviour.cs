using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlimeBehaviour : UnitBehaviour
{
    public int meleeDamage;
    public ParticleSystem attackAnimation;
    public override void act() {
        GridPosition gridpos = GetComponent<GridPosition>();
        if (gridpos.row > 1) {
            gridpos.row -= 1;
        } else {
            dealMeleeDamage(gridpos.column);
        }
    }

    void dealMeleeDamage(int column) {
        // damage player
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.takeDamage(meleeDamage, column);
        attackAnimation.Play();
    }


}
