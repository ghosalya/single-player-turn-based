using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public PlayerController playerController;
    public EnemySquad enemySquad;

    [SerializeField]
    GameObject winPanel, losePanel;

    public bool playerPhase { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        startTurn();
    }

    // Update is called once per frame
    void Update()
    {
        winPanel.SetActive(victory());
        losePanel.SetActive(defeat());
    }

    public void startTurn()
    {
        playerPhase = true;
        playerController.OnTurnStart();
        enemySquad.OnTurnStart();
        // gameObject.SendMessage("OnTurnStart");
    }

    public void endTurn()
    {
        playerPhase = false;
        playerController.OnTurnEnd();
        enemySquad.OnTurnEnd();
        // gameObject.SendMessage("OnTurnEnd");
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

    public void returnToWorld() {
        SceneManager.LoadScene("Overworld");
    }


}
