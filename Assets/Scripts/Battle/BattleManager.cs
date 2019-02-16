using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public PlayerController playerController;
    public EnemySquad enemySquad;

    public bool playerPhase { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        startTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTurn()
    {
        playerPhase = true;
        SendMessage("OnTurnStart");
    }

    public void endTurn()
    {
        playerPhase = false;
        SendMessage("OnTurnEnd");
    }

    public bool victory()
    {
        foreach(GameObject enemy in enemySquad.enemies)
        {
            if(enemy.GetComponent<UnitHealth>().health > 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool defeat()
    {
        return playerController.health <= 0;
    }


}
