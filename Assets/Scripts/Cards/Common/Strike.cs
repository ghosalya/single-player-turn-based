using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Strike : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Strike";
        card.description = "Deals 40 damage to the first unit in a lane.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 30;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 40, 0, 5),
        };
        return card;
    }
}

public class StrikeEffect : Effect
{
    int damage, knockback, range;
    public StrikeEffect(Card card, int damage, int knockback, int range) : base(card) {
        this.damage = damage;
        this.knockback = knockback;
        this.range = Mathf.Min(range, 5);
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();


        int column = pcon.cellSelected[0];
        GameObject enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
        if(enemy != null) {
            if (enemy.GetComponent<GridPosition>().row <= range) {
                Debug.Log(card.cardName + ": Damaging enemy at " + enemy.GetComponent<GridPosition>().row + ',' + enemy.GetComponent<GridPosition>().column);
                int finalDamage = pcon.getModifiedDamage(damage);
                enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
                esquad.knockEnemyUp(enemy, knockback);
                battle.GetComponent<AnimationController>().add(new StrikeAnim(column));
            } else {
                Debug.Log(card.cardName + ": Missed (no enemy in range " + range.ToString() + ").");
            }
        } else {
            Debug.Log(card.cardName + ": Target not found.");
        }
    }
}

public class StrikeAnim : CardAnimation
{ 
    int column;

    public StrikeAnim(int column) {
        this.column = column;
    }

    public override void animate() {
        float x = GridPosition.columnXOffset + GridPosition.columnXFactor * column;
        GameObject prefab = Resources.Load<GameObject>("Animation\\StrikeBullet");
        GameObject instance = Object.Instantiate(prefab, new Vector3(x, 0, -40), Quaternion.identity);
        instance.SetActive(true);
    }
}