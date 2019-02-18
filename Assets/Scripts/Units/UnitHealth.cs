using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public int maxHealth { get; private set; }
    public int health { get; private set; }
    public int block { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        block = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        SendMessage("OnTakingDamage", damage);
        int damageToHealth = damage - block;
        if(damageToHealth <= 0)
        {
            block = block - damage;
        } else
        {
            block = 0;
            health = Mathf.Clamp(health - damageToHealth, 0, maxHealth);
        }
    }

    public void gainHealth(int heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
    }

    public void gainBlock(int blockGain)
    {
        block += blockGain;
    }
}
