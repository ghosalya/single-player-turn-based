using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownSlimeBehaviour : UnitBehaviour
{
    public int meleeDamage;
    public int blockGain;
    public ParticleSystem attackAnimation;
    public GameObject slimeling;
    int turnCounter = 0;
    
    public void Start() {
        playMonsterSFX();
    }

    public override void act() {
        if (turnCounter == 0) {
            moveAndSpawnOrAttack();
        } else if (turnCounter == 1) {
            moveOrAttack();
        } else if (turnCounter == 2) {
            UnitHealth health = GetComponent<UnitHealth>();
            health.gainBlock(blockGain);
        }

        turnCounter = (turnCounter + 1) % 3;
    }


    void moveAndSpawnOrAttack() {
        GridPosition gridpos = GetComponent<GridPosition>();
        if (gridpos.row > 1) {
            spawnSlimeling(gridpos.row, gridpos.column);
            gridpos.row -= 1;
        } else {
            dealMeleeDamage(gridpos.column);
        }
    }

    void spawnSlimeling(int row, int column) {
        slimeling.transform.position = gameObject.transform.position;
        EnemySquad esquad = GameObject.FindGameObjectWithTag("Battle").GetComponent<EnemySquad>();
        esquad.summon(slimeling, row, column);
    }

    void moveOrAttack() {
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
