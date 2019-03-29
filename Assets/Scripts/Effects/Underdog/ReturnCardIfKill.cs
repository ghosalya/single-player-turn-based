using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/ReturnCardIfKill")]
public class ReturnCardIfKill : Effect
{
    public bool returnIfKill = true;
    public bool targetLast = true;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];
        GameObject enemy;
        if(targetLast) {
            enemy = battle.GetComponent<EnemySquad>().getLastEnemyInColumn(column);
        } else {
            enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
        }
        if(enemy != null) {
            int enemyhealth = enemy.GetComponent<UnitHealth>().health;
            if((enemyhealth > 0 && !returnIfKill) || (enemyhealth <= 0 && returnIfKill)) {
                // return card
                pcon.handCards.Remove(this.card);
                pcon.drawPile.Insert(Random.Range(0, pcon.drawPile.Count), this.card);
            }
        }
    }
}
