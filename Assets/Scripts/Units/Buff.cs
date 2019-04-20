using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string displayName;
    public bool temporary;
    public int stack;
    public string description;

    public void ConsumeStack() {
        stack -= 1;
        OnConsumed();
    }

    public void ConsumeStackOnTurnEnd() {
        if(temporary) { ConsumeStack(); }
    }

    public Buff clone() {
        Buff clone = Instantiate(this);
        return clone;
    }

    public virtual void OnEvent(string eventID) {}
    // Override this to have something happen in response
    // to an event. Possible events include: OnTurnStart, OnTurnEnd etc

    public virtual void OnApplied() {}
    // Override this to do something when this buff is applied

    public virtual void OnConsumed() {}
    // Override this to do something when a stack is consumed

    public virtual void OnRemoved() {}
    // Override this to do something when this buff is removed

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
