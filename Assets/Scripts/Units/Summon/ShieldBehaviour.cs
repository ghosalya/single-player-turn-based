using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : UnitBehaviour
{
    public int meleeDamage;
    public ParticleSystem attackAnimation;

    public void Start() {
        
    }

    public void Update() {
        GetComponent<UnitHealthbar>().UpdateUI();
    }

    public override void act() {
        // its a shield, it does nothing.
    }
}
