using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlimeBehaviour : UnitBehaviour
{
    public int rangeDamage;
    public int desiredRow;
    public ParticleSystem attackAnimation;
    public override void act() {
        moveOrAttack();
    }

    void moveOrAttack() {
        GridPosition gridpos = GetComponent<GridPosition>();
        if (gridpos.row > desiredRow) {
            gridpos.row -= 1;
        } else if (gridpos.row < desiredRow) {
            gridpos.row += 1;
        } else {
            dealRangedDamage(gridpos.column);
        }
    }


    void dealRangedDamage(int column) {
        // damage player
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.takeDamage(rangeDamage, column);
        attackAnimation.Play();
    }
}
