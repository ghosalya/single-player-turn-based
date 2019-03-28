using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public bool temporary;
    public int stack;
    public string description;

    public void ConsumeStack() {
        stack -= 1;
    }

    public void ConsumeStackOnTurnEnd() {
        if(temporary) { ConsumeStack(); }
    }

    public virtual void OnEvent(string eventID) {}
    // Override this to have something happen in response
    // to an event. Possible events include: OnTurnStart, OnTurnEnd etc

    public virtual int AddBonusDamage(int damage)
    {
        return damage;
    }
    public virtual int MultiplyDamage(int damage)
    {
        return damage;
    }

    public virtual int AddBonusBlock(int block) {
        return block;
    }

    public virtual int MultiplyBlock(int block) {
        return block;
    }
}
