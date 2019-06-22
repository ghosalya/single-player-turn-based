using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlimeBehaviour : UnitBehaviour
{
    public int rangeDamage;
    public int desiredRow;
    public ParticleSystem attackAnimation;
    
    public void Start() {
        playMonsterSFX();
    }

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
        playMonsterSFX();
        // damage player
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.takeDamage(rangeDamage, column);
        attackAnimation.Play();
    }
    
    void playMonsterSFX() {
        GameObject hitSFX = GameObject.Find("MonsterSFX");
        if (hitSFX != null) {
            hitSFX.GetComponent<AudioSource>().Play();
        }
    }
}
