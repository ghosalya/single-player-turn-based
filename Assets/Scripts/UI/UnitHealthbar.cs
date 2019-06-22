using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthbar : MonoBehaviour
{
    public static float XScale = 0.15f;
    public static float ZScale = 0.0075f;
    public Transform healthbarPlane;
    public TextMesh healthbarNumber;
    public Transform blockPlane;
    public TextMesh blockNumber;

    // Start is called before the first frame update
    void Start()
    {
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // updateHealth();
        // healthbarPlane.LookAt(Camera.main.transform);
    }

    void updateHealth()
    {
        UnitHealth unitHealth = GetComponent<UnitHealth>();
        healthbarPlane.localScale = new Vector3(XScale * unitHealth.health / unitHealth.maxHealth, 1, ZScale);
        healthbarNumber.text = unitHealth.health.ToString();

        if(unitHealth.block <= 0) {
            blockPlane.gameObject.SetActive(false);
            blockNumber.gameObject.SetActive(false);
        } else {
            blockPlane.gameObject.SetActive(true);
            blockNumber.gameObject.SetActive(true);
            blockNumber.text = unitHealth.block.ToString();
        }

        if (unitHealth.health <= 0) {
            GameObject battle = GameObject.FindGameObjectWithTag("Battle");
            battle.SendMessage("OnKill");
            animateDeath();
        }
    }

    void animateDeath() {
        if (GetComponent<UnitBehaviour>().affiliation == UnitBehaviour.UnitAffiliation.ENEMY) {
            EnemySquad squad = GameObject.FindGameObjectWithTag("Battle").GetComponent<EnemySquad>();
            squad.enemies.Remove(gameObject);  // remove this from squad
        } else if (GetComponent<UnitBehaviour>().affiliation == UnitBehaviour.UnitAffiliation.FRIENDLY) {
            PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
            pcon.summons.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public void UpdateUI()
    {
        updateHealth();
    }
}
