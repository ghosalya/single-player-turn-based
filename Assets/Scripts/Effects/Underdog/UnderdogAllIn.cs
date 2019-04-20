using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Underdog/AllIn")]
public class UnderdogAllIn : Effect
{
    public Card.CardType discardedCardType;
    public int damage = 10;
    public int knockback = 0;
    public GameObject prefab;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        
        foreach(Card card in pcon.handCards) {
            if(card.type == discardedCardType && card != this.card) {
                pcon.discard(card);
                damageRandomLane();
            }
        }
    }

    public void damageRandomLane() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();
        int column = Random.Range(0, 4);
        GameObject enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
        if(enemy != null) {
            int finalDamage = pcon.getModifiedDamage(damage);
            enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
            esquad.knockEnemyUp(enemy, knockback);
        }

        spawnAnimation(column);
    }

    public void spawnAnimation(int column)
    {
        float x = GridPosition.columnXOffset + GridPosition.columnXFactor * column;
        Instantiate(prefab, new Vector3(x, 0, -40), Quaternion.identity);
    }
}
