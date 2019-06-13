using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class SubtleStrike : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Subtle Strike";
        card.description = "Deals 50 damage to the last enemy in the lane and push it forward.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 35;
        card.effects = new List<Effect>(){
            new BackStrikeEffect(card, 50, 1, 5),
        };
        return card;
    }
}

public class BackStrikeEffect : Effect
{
    int damage, knockback, range;
    public BackStrikeEffect(Card card, int damage, int knockback, int range) : base(card) {
        this.damage = damage;
        this.knockback = knockback;
        this.range = Mathf.Min(range, 5);
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();


        int column = pcon.cellSelected[0];
        GameObject enemy = battle.GetComponent<EnemySquad>().getLastEnemyInColumn(column);
        if(enemy != null) {
            if (5 - enemy.GetComponent<GridPosition>().row <= range) {
                Debug.Log(card.cardName + ": Damaging enemy at " + enemy.GetComponent<GridPosition>().row + ',' + enemy.GetComponent<GridPosition>().column);
                int finalDamage = pcon.getModifiedDamage(damage);
                enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
                esquad.knockEnemyUp(enemy, -knockback);
            } else {
                Debug.Log(card.cardName + ": Missed (no enemy in range " + range.ToString() + ").");
            }
            battle.GetComponent<AnimationController>().add(new BackStrikeAnim(column, 5));
        } else {
            Debug.Log(card.cardName + ": Target not found.");
        }
    }
}

public class BackStrikeAnim : CardAnimation
{ 
    int column;
    int lineOffset = 0;

    public BackStrikeAnim(int column) {
        this.column = column;
    }

    public BackStrikeAnim(int column, int lineOffset) {
        // The bigger the offset, the later it will show
        this.column = column;
        this.lineOffset = lineOffset;
    }

    public override void animate() {
        float x = GridPosition.columnXOffset + GridPosition.columnXFactor * column;
        GameObject prefab = Resources.Load<GameObject>("Animation\\StrikeBullet");
        GameObject instance = Object.Instantiate(prefab, new Vector3(x, 0, 40 + lineOffset), Quaternion.Euler(0, 180, 0));
        instance.SetActive(true);
    }
}