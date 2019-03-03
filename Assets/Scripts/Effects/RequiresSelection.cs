using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiresSelection : Effect
{
    public override void activate()
    {
        GameObject.FindGameObjectWithTag("SelectionCircle").SetActive(true);
    }
}
