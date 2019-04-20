using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DamageAllEnemies")]
public class DamageAllEnemies : Effect
{
    public int damage = 10;
    public int knockback = 0;
    public GameObject prefab;

    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();

        int finalDamage = pcon.getModifiedDamage(damage);
        foreach (GameObject enemy in esquad.enemies) {
            enemy.GetComponent<UnitHealth>().takeDamage(finalDamage);
        }

        spawnAnimation();
    }

    public void spawnAnimation()
    {
        Instantiate(prefab, new Vector3(0, 0, -40), Quaternion.identity);
    }
}
