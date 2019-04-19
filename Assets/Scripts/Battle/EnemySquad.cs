using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{
    public List<GameObject> enemies;

    public GameObject knockbackClashAnimation;

    public EnemySquadSet[] possibleSquadSet;

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
        // make a new list so that summoned units dont act this turn
        List<GameObject> enemiesToAct = new List<GameObject>();
        foreach(GameObject enemy in enemies) {
            enemiesToAct.Add(enemy);
        }

        foreach(GameObject enemyObject in enemiesToAct)
        {
            enemyObject.GetComponent<UnitBehaviour>().act();
            enemyObject.SendMessage("UpdateUI");
        }
        
        BattleManager battleManager = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleManager>();
        battleManager.startTurn();
    }

    public void deploy()
    {
        EnemySquadSet chosenSquadSet = possibleSquadSet[Random.Range(0, possibleSquadSet.Length)];

        List<GameObject> deployedEnemies = new List<GameObject>();
        for (int i = 0; i < chosenSquadSet.enemiesToDeploy.Count; i++)
        {
            // instantiating
            GameObject enemyPrefab = chosenSquadSet.enemiesToDeploy[i];
            GameObject deployedEnemy = Instantiate(enemyPrefab);
            deployedEnemies.Add(deployedEnemy);

            // setting GridPosition
            Vector2 position = chosenSquadSet.enemyPosition[i];
            deployedEnemy.GetComponent<GridPosition>().column = (int) position.x;
            deployedEnemy.GetComponent<GridPosition>().row = (int) position.y;
        }

        enemies = deployedEnemies;
    }

    public void summon(GameObject enemyPrefab, int row, int column) {
        GameObject summonedEnemy = Instantiate(enemyPrefab);
        enemies.Add(summonedEnemy);
        
        GridPosition gridpos = summonedEnemy.GetComponent<GridPosition>();
        gridpos.column = column;
        gridpos.row = row;
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
            if(nextRow < 1 || nextRow > 5) {
                // edge of the field
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                return;
            }

            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().row += 1;
                knockEnemyUp(enemy, knockback - 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Vector3 clashPosition = new Vector3(
                    (enemy.transform.position.x + enemyAtSlot.transform.position.x) / 2,
                    3,
                    (enemy.transform.position.z + enemyAtSlot.transform.position.z) / 2
                );
                GameObject clash = Instantiate(knockbackClashAnimation, clashPosition, Quaternion.identity);
                Destroy(clash, 1);
                Debug.Log("Knock Damage!");
            }
        } else if (knockback < 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row - 1;
            int nextColumn = enemy.GetComponent<GridPosition>().column;
            if(nextRow < 1 || nextRow > 5) {
                // edge of the field
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                return;
            }

            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().row -= 1;
                knockEnemyUp(enemy, knockback + 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Vector3 clashPosition = new Vector3(
                    (enemy.transform.position.x + enemyAtSlot.transform.position.x) / 2,
                    3,
                    (enemy.transform.position.z + enemyAtSlot.transform.position.z) / 2
                );
                GameObject clash = Instantiate(knockbackClashAnimation, clashPosition, Quaternion.identity);
                Destroy(clash, 1);
                Debug.Log("Knock Damage!");
            }
        }
    }
    
    public void knockEnemyRight(GameObject enemy, int knockback) {
        if (knockback == 0) return;
        if (knockback > 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row;
            int nextColumn = enemy.GetComponent<GridPosition>().column + 1;
            if(nextColumn < 0 || nextColumn > 3) {
                // edge of the field
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                return;
            }

            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().column += 1;
                knockEnemyUp(enemy, knockback - 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Vector3 clashPosition = new Vector3(
                    (enemy.transform.position.x + enemyAtSlot.transform.position.x) / 2,
                    3,
                    (enemy.transform.position.z + enemyAtSlot.transform.position.z) / 2
                );
                GameObject clash = Instantiate(knockbackClashAnimation, clashPosition, Quaternion.identity);
                Destroy(clash, 1);
                Debug.Log("Knock Damage!");
            }
        } else if (knockback < 0) {
            int nextRow = enemy.GetComponent<GridPosition>().row;
            int nextColumn = enemy.GetComponent<GridPosition>().column - 1;
            if(nextColumn < 0 || nextColumn > 3) {
                // edge of the field
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                return;
            }

            GameObject enemyAtSlot = getEnemyAtPosition(nextColumn, nextRow);
            if(enemyAtSlot == null) {
                enemy.GetComponent<GridPosition>().column -= 1;
                knockEnemyUp(enemy, knockback + 1);
            } else {
                // knock to someone else, both take 40 damage
                enemy.GetComponent<UnitHealth>().takeDamage(40);
                enemyAtSlot.GetComponent<UnitHealth>().takeDamage(40);
                Vector3 clashPosition = new Vector3(
                    (enemy.transform.position.x + enemyAtSlot.transform.position.x) / 2,
                    3,
                    (enemy.transform.position.z + enemyAtSlot.transform.position.z) / 2
                );
                GameObject clash = Instantiate(knockbackClashAnimation, clashPosition, Quaternion.identity);
                Destroy(clash, 1);
                Debug.Log("Knock Damage!");
            }
        }
    }
}
