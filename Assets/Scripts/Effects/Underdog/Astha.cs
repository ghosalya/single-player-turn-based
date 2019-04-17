using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Underdog/Astha")]
public class Astha : Effect
{
    public Card[] cardsToCheck;
    public Card cardToAdd;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        
        foreach(Card card in cardsToCheck) {
            if(cardNameIsInTurnHistory(pcon, card)) {
                Debug.Log(this.name + ": Card " + card.cardName + " found.");
            } else {
                Debug.Log(this.name + ": Card " + card.cardName + " not found.");
                return;
            }
        }

        Debug.Log(this.name + ": All cards found; adding " + cardToAdd.cardName);
        pcon.handCards.Add(cardToAdd.clone());
    }

    public bool cardNameIsInTurnHistory(PlayerController pcon, Card checkedCard) {
        foreach(Card card in pcon.turnHistory) {
            if(card.cardName == checkedCard.cardName) {
                return true;
            }
        }
        return false;
    }
}
