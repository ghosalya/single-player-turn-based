using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMagusBehaviour : UnitBehaviour
{
    public int attackDamage;
    public int selfHeal;
    public ParticleSystem attackAnimation;
    public ParticleSystem healAnimation;
    public GameObject[] slimeToSummon;
    public int turnCounter = 0;
    public override void act() {
        if (turnCounter == 0) {
            summonRandomSlime();
        } else if (turnCounter == 1) {
            healRandomSlime();
        } else if (turnCounter == 2) {
            healSelf();
        } else if (turnCounter == 3) {
            attack();
        }

        turnCounter = (turnCounter + 1) % 4;
    }

    void summonRandomSlime() {
        EnemySquad esquad = GameObject.FindGameObjectWithTag("Battle").GetComponent<EnemySquad>();
        GameObject toSummon = slimeToSummon[Random.Range(0, slimeToSummon.Length)];
        GridPosition gridpos = GetComponent<GridPosition>();
        toSummon.transform.position = gameObject.transform.position;
        esquad.summon(toSummon, gridpos.row - 1, gridpos.column);
    }

    void healRandomSlime() {
        EnemySquad esquad = GameObject.FindGameObjectWithTag("Battle").GetComponent<EnemySquad>();
        List<GameObject> slimesToHeal = new List<GameObject>();
        foreach(GameObject enemy in esquad.enemies) {
            if (
                enemy.GetComponent<RedSlimeBehaviour>() != null
                || enemy.GetComponent<BlueSlimeBehaviour>() != null
                || enemy.GetComponent<GreenSlimeBehaviour>() != null
                || enemy.GetComponent<BrownSlimeBehaviour>() != null
                || enemy.GetComponent<BrownSlimelingBehaviour>() != null
            ) {
                slimesToHeal.Add(enemy);
            }
        }
        GameObject slimeTarget = slimesToHeal[Random.Range(0, slimesToHeal.Count)];
        healAnimation.transform.position = slimeTarget.transform.position;
        healAnimation.transform.parent = slimeTarget.transform;
        slimeTarget.GetComponent<UnitHealth>().gainHealth(99999);
        healAnimation.Play();
    }

    void healSelf() {
        healAnimation.transform.position = gameObject.transform.position;
        GetComponent<UnitHealth>().gainHealth(selfHeal);
        healAnimation.transform.parent = transform;
        healAnimation.Play();
    }

    void attack() {
        // damage player
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        GridPosition gridpos = GetComponent<GridPosition>();
        pcon.takeDamage(attackDamage, gridpos.column);
        attackAnimation.Play();
    }
}
