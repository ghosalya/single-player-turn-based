using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DamageFirstIfCardIsPlayed")]
public class DamageFirstIfCardIsPlayed : Effect
{
    public GameObject prefab;
    public int damage;
    public int knockback = 1;

    public override void activate() {
        if(cardIsPlayedThisTurn()) {
            GameObject battle = GameObject.FindGameObjectWithTag("Battle");
            PlayerController pcon = battle.GetComponent<PlayerController>();
            int column = pcon.cellSelected[0];
            GameObject enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
            if(enemy != null) {
                int finalDamage = pcon.getModifiedDamage(damage);
                enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
                enemy.GetComponent<GridPosition>().row += knockback;
            }
            spawnAnimation(column);
        }
    }
    public void spawnAnimation(int column)
    {
        float x = GridPosition.columnXOffset + GridPosition.columnXFactor * column;
        Instantiate(prefab, new Vector3(x, 0, -40), Quaternion.identity);
    }

    public bool cardIsPlayedThisTurn() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        foreach(Card c in pcon.turnHistory) {
            if(c.cardName == this.card.cardName) { return true; }
        }
        return false;
    }
}
