using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DamageFirstInLane")]
public class DamageFirstInLane : Effect
{
    public int damage = 10;
    public GameObject prefab;

    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];
        GameObject enemy = battle.GetComponent<EnemySquad>().getFirstEnemyInColumn(column);
        enemy.GetComponent<UnitHealth>().takeDamage(damage);

        spawnAnimation(column);
    }

    public void spawnAnimation(int column)
    {
        float x = GridPosition.columnXOffset + GridPosition.columnXFactor * column;
        Instantiate(prefab, new Vector3(x, 0, -40), Quaternion.identity);
    }
}
