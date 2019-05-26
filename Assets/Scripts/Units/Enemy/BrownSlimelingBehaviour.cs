using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownSlimelingBehaviour : UnitBehaviour
{
    public int blockGain;
    public GameObject evolution;
    public ParticleSystem evolutionAnimation;
    int turnCounter = 0;
    public override void act() {
        if (turnCounter == 0) {
            UnitHealth health = GetComponent<UnitHealth>();
            health.gainBlock(blockGain);
        } else if (turnCounter == 1) {
            evolve();
        }

        turnCounter = (turnCounter + 1) % 2;
    }

    void evolve() {
        evolution.transform.position = gameObject.transform.position;
        GridPosition gridpos = GetComponent<GridPosition>();
        EnemySquad esquad = GameObject.FindGameObjectWithTag("Battle").GetComponent<EnemySquad>();
        esquad.summon(evolution, gridpos.row, gridpos.column);
        esquad.enemies.Remove(gameObject);
        evolutionAnimation.Play();
        Destroy(gameObject, 0.5f);
    }
}
