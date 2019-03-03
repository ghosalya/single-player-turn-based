using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardBehaviour : UnitBehaviour
{
    public override void act() {
        GridPosition gridpos = GetComponent<GridPosition>();
        if (gridpos.row > 1) {
            gridpos.row -= 1;
        }
    }
}
