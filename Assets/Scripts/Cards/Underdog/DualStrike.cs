using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class DualStrike : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Dual Strike";
        card.description = "Deals 30 damage to the first unit in a lane with 1 knockback. Deals another 40 damage to the first unit in a random lane.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 45;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 30, 1, 5),
            new RandomStrikeEffect(card, 30, 0, 5),
        };
        return card;
    }
}

public class RandomStrikeEffect : Effect
{
    int damage, knockback, range;
    public RandomStrikeEffect(Card card, int damage, int knockback, int range) : base(card) {
        this.damage = damage;
        this.knockback = knockback;
        this.range = Mathf.Min(range, 5);
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();


        int column = Random.Range(0, 4);
        GameObject enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
        if(enemy != null) {
            if (enemy.GetComponent<GridPosition>().row <= range) {
                Debug.Log(card.cardName + ": Damaging enemy at " + enemy.GetComponent<GridPosition>().row + ',' + enemy.GetComponent<GridPosition>().column);
                int finalDamage = pcon.getModifiedDamage(damage);
                enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
                esquad.knockEnemyUp(enemy, knockback);
            } else {
                Debug.Log(card.cardName + ": Missed (no enemy in range " + range.ToString() + ").");
            }
            battle.GetComponent<AnimationController>().add(new StrikeAnim(column, 5));
        } else {
            Debug.Log(card.cardName + ": Target not found.");
        }
    }
}
