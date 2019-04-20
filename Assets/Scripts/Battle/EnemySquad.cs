using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Vector2> enemySpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        deploy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void act()
    {
        foreach(GameObject enemyObject in enemies)
        {
            enemyObject.GetComponent<UnitBehaviour>().act();
            enemyObject.SendMessage("UpdateUI");
        }
        
        BattleManager battleManager = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleManager>();
        battleManager.startTurn();
    }

    public void deploy()
    {
        List<GameObject> deployedEnemies = new List<GameObject>();
        for (int i = 0; i < enemies.Count; i++)
        {
            // instantiating
            GameObject enemyPrefab = enemies[i];
            GameObject deployedEnemy = Instantiate(enemyPrefab);
            deployedEnemies.Add(deployedEnemy);

            // setting GridPosition
            Vector2 position = enemySpawnPosition[i];
            deployedEnemy.GetComponent<GridPosition>().column = (int) position.x;
            deployedEnemy.GetComponent<GridPosition>().row = (int) position.y;
        }

        enemies = deployedEnemies;
    }

    public GameObject getFirstEnemyInColumn(int column)
    {
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            GridPosition enemyPos = enemy.GetComponent<GridPosition>();
            if(enemyPos.column == column)
            {
                if(nearestEnemy == null) { nearestEnemy = enemy; }
                else if(nearestEnemy.GetComponent<GridPosition>().row > enemyPos.row)
                {
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    public GameObject getLastEnemyInColumn(int column)
    {
        GameObject furthestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            GridPosition enemyPos = enemy.GetComponent<GridPosition>();
            if(enemyPos.column == column)
            {
                if(furthestEnemy == null) { furthestEnemy = enemy; }
                else if(furthestEnemy.GetComponent<GridPosition>().row < enemyPos.row)
                {
                    furthestEnemy = enemy;
                }
            }
        }

        return furthestEnemy;
    }

    public void OnTurnStart()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SendMessage("OnTurnStart");
        }
    }

    public void OnTurnEnd()
    {
        act();
        foreach(GameObject enemy in enemies)
        {
            enemy.SendMessage("OnTurnEnd");
        }
    }

    public void UpdateUI() {
        foreach(GameObject enemy in enemies) {
            enemy.SendMessage("UpdateUI");
        }
    }

    public GameObject getEnemyAtPosition(int x, int y) {
        foreach(GameObject enemy in enemies) {
            GridPosition enemypos = enemy.GetComponent<GridPosition>();
            if(enemypos.column == x && enemypos.row == y) {
                return enemy;
            }
        }
        return null;
    }

    public void knockEnemyUp(GameObject enemy, int knockback) {
        if (knockback == 0) return;
        if (knockback > 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row + 1;
            int nextColumn = enemy.GetComponent<GridPosition>().column;
            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().row += 1;
                knockEnemyUp(enemy, knockback - 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Debug.Log("Knock Damage!");
            }
        } else if (knockback < 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row - 1;
            int nextColumn = enemy.GetComponent<GridPosition>().column;
            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().row -= 1;
                knockEnemyUp(enemy, knockback + 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Debug.Log("Knock Damage!");
            }
        }
    }
    
    public void knockEnemyRight(GameObject enemy, int knockback) {
        if (knockback == 0) return;
        if (knockback > 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row;
            int nextColumn = enemy.GetComponent<GridPosition>().column + 1;
            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().column += 1;
                knockEnemyUp(enemy, knockback - 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Debug.Log("Knock Damage!");
            }
        } else if (knockback < 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row;
            int nextColumn = enemy.GetComponent<GridPosition>().column - 1;
            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().column -= 1;
                knockEnemyUp(enemy, knockback + 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Debug.Log("Knock Damage!");
            }
        }
    }
}
