using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class RicochetStrike : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Ricochet Strike";
        card.description = "Deals 30 damage to the leftmost and rightmost enemies in a row and push them inwards.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 40;
        card.effects = new List<Effect>(){
            new SideStrikeEffect(card, 30, 1, 5, SideStrikeEffect.Direction.left),
            new SideStrikeEffect(card, 30, 1, 5, SideStrikeEffect.Direction.right),
        };
        return card;
    }
}

public class SideStrikeEffect : Effect
{
    public enum Direction { left, right, }
    int damage, knockback, range;
    Direction attackFrom;
    PlayerController pcon;
    EnemySquad esquad;
    public SideStrikeEffect(Card card, int damage, int knockback, int range, Direction attackFrom) : base(card) {
        this.damage = damage;
        this.knockback = knockback;
        this.range = Mathf.Min(range, 5);
        this.attackFrom = attackFrom;
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        pcon = battle.GetComponent<PlayerController>();
        esquad = battle.GetComponent<EnemySquad>();

        int row = pcon.cellSelected[1];
        attack(row);
    }

    public GameObject getOutermostEnemy(Direction direction, int row) {
        GameObject target = null;
        foreach (GameObject enemy in esquad.enemies) {
            GridPosition position = enemy.GetComponent<GridPosition>();
            if (position == null) { continue; }
            if (position.row != row) { continue; }
            if (target == null) { target = enemy; }
            else {
                GridPosition targetPosition = target.GetComponent<GridPosition>();
                if (direction == Direction.left) {
                    if (targetPosition.column > position.column) { target = enemy;}
                } else if (direction == Direction.right) {
                    if (targetPosition.column < position.column) { target = enemy;}
                }
            }
        }
        return target;
    }

    public bool enemyInRange(GameObject enemy) {
        GridPosition position = enemy.GetComponent<GridPosition>();
        if ( attackFrom == Direction.left) {
            return (position.column < range);
        } else {
            return (4 - position.column < range);
        }
    }

    public void attack(int row) {
        GameObject enemy = getOutermostEnemy(attackFrom, row);
        if(enemy != null) {
            if (enemyInRange(enemy)) {
                Debug.Log(card.cardName + ": Damaging enemy at " + enemy.GetComponent<GridPosition>().row + ',' + enemy.GetComponent<GridPosition>().column);
                int finalDamage = pcon.getModifiedDamage(damage);
                enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
                if (attackFrom == Direction.left) {
                    esquad.knockEnemyRight(enemy, knockback);
                } else {
                    esquad.knockEnemyRight(enemy, -knockback);
                }
            } else {
                Debug.Log(card.cardName + ": Missed (no enemy in range " + range.ToString() + ").");
            }
            esquad.gameObject.GetComponent<AnimationController>().add(new SideStrikeAnim(row, attackFrom));
        } else {
            Debug.Log(card.cardName + ": Target not found.");
        }
    }
}

public class SideStrikeAnim : CardAnimation
{ 
    int row;
    int lineOffset = 0;
    SideStrikeEffect.Direction attackFrom;

    public SideStrikeAnim(int row, SideStrikeEffect.Direction attackFrom) {
        this.row = row;
        this.attackFrom = attackFrom;
    }

    public SideStrikeAnim(int row, SideStrikeEffect.Direction attackFrom, int lineOffset) {
        // The bigger the offset, the later it will show
        this.row = row;
        this.attackFrom = attackFrom;
        this.lineOffset = lineOffset;
    }

    public override void animate() {
        float y = GridPosition.rowYOffset + GridPosition.rowYFactor * row;
        float offset, rotation;
        if (attackFrom == SideStrikeEffect.Direction.left) {
            offset = -40 - lineOffset;
            rotation = 90;
        } else {
            offset = 40 + lineOffset;
            rotation = -90;
        }
        GameObject prefab = Resources.Load<GameObject>("Animation\\StrikeBullet");
        GameObject instance = Object.Instantiate(prefab, new Vector3(offset, 0, y), Quaternion.Euler(0, rotation, 0));
        instance.SetActive(true);
    }
}