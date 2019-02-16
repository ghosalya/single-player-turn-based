using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour
{
    public enum UnitAffiliation
    {
        FRIENDLY,
        NEUTRAL,
        ENEMY
    }

    public UnitAffiliation affiliation;
    public HashSet<Buff> buffs;

    public abstract void act();
}
