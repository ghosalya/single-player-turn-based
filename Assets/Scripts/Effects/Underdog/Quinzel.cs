using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Underdog/Quinzel")]
public class Quinzel : Effect
{
    public Card.CardType bonusDrawRequiredCard;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        
        if(pcon.discardPile.Count == 0) {
            Debug.Log(this.name + ": Nulled - no cards in discard pile");
            return;
        }

        Card cardFromDiscardPile = pcon.discardPile[Random.Range(0, pcon.discardPile.Count)];
        pcon.handCards.Add(cardFromDiscardPile);
        pcon.discardPile.Remove(cardFromDiscardPile);

        if(cardFromDiscardPile.type == bonusDrawRequiredCard) {
            Debug.Log(this.name + ": Card type from discard pile matches. Drawing 1 card.");
            pcon.draw(1);
        }
    }
}
