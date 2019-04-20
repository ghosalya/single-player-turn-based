using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DrawCard")]
public class DrawCard : Effect
{
    public int cardsDrawn;
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.draw(cardsDrawn);
    }
}
