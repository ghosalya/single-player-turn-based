using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Effect/DamageSideMostInRow")]
public class DamageSideMostInRow : Effect
{
    public enum Side {left, right}
    public Side attackFrom;
    public int damage;
    public int knockback;
    public GameObject prefab;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();

        int rowSelected = pcon.cellSelected[1];
        GameObject targetEnemy = getSidemostEnemy(rowSelected, esquad);
        if(targetEnemy != null) {
            int finalDamage = pcon.getModifiedDamage(damage);
            targetEnemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
            if(attackFrom == Side.left) {
                esquad.knockEnemyRight(targetEnemy, knockback);
            } else {
                esquad.knockEnemyRight(targetEnemy, knockback * -1);
            }
        }
        spawnAnimation(rowSelected);
    }

    public void spawnAnimation(int row)
    {
        float y = GridPosition.rowYOffset + GridPosition.rowYFactor * row;
        if(attackFrom == Side.left) {
            Instantiate(prefab, new Vector3(-40, 0, y), Quaternion.identity);
        } else {
            Instantiate(prefab, new Vector3(40, 0, y), Quaternion.identity);
        }
    }

    public GameObject getSidemostEnemy(int row, EnemySquad esquad) {
        GameObject targetEnemy = null;
        foreach (GameObject enemy in esquad.enemies) {
            if(enemy.GetComponent<GridPosition>().row == row) {
                if (targetEnemy == null) { 
                    targetEnemy = enemy;
                    continue;
                }
                int columnDiff = enemy.GetComponent<GridPosition>().column - targetEnemy.GetComponent<GridPosition>().column;
                if ((attackFrom == Side.left && columnDiff < 0) || (attackFrom == Side.right && columnDiff > 0)) {
                    targetEnemy = enemy;
                }
            }
            
        }
        return targetEnemy;
    }
}
