using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Vector2> enemyPosition;

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
            Vector2 position = enemyPosition[i];
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
                else if(nearestEnemy.GetComponent<GridPosition>().column > enemyPos.column)
                {
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
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
}
