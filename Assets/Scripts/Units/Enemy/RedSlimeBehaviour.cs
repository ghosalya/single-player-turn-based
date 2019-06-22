using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlimeBehaviour : UnitBehaviour
{
    public int meleeDamage;
    public ParticleSystem attackAnimation;

    public void Start() {
        playMonsterSFX();
    }

    public override void act() {
        GridPosition gridpos = GetComponent<GridPosition>();
        if (gridpos.row > 1) {
            gridpos.row -= 1;
        } else {
            dealMeleeDamage(gridpos.column);
        }
    }

    void dealMeleeDamage(int column) {
        playMonsterSFX();
        // damage player
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.takeDamage(meleeDamage, column);
        attackAnimation.Play();
    }


    void playMonsterSFX() {
        GameObject hitSFX = GameObject.Find("MonsterSFX");
        if (hitSFX != null) {
            hitSFX.GetComponent<AudioSource>().Play();
        }
    }
}
