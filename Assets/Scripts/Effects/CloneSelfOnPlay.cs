using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/CloneSelfOnPlay")]
public class CloneSelfOnPlay : Effect
{
    public PlayerController.CardPile targetPile;
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.getPile(targetPile).Add(this.card.clone());
    }
}
