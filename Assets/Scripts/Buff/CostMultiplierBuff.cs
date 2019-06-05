using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buff/CostMultiplerBuff")]
public class CostMultiplierBuff : Buff
{
    public Card.CardType cardType;
    public float multiplier;

    public override void OnApplied() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        
        foreach(Card card in pcon.drawPile) {
            if(card.type == cardType) {
                card.costMultiplier *= multiplier;
            }
        }
        
        foreach(Card card in pcon.handCards) {
            if(card.type == cardType) {
                card.costMultiplier *= multiplier;
            }
        }
        
        foreach(Card card in pcon.discardPile) {
            if(card.type == cardType) {
                card.costMultiplier *= multiplier;
            }
        }
    }
    
    public override void OnRemoved() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        
        foreach(Card card in pcon.drawPile) {
            if(card.type == cardType) {
                card.restoreCost();
            }
        }
        
        foreach(Card card in pcon.handCards) {
            if(card.type == cardType) {
                card.restoreCost();
            }
        }
        
        foreach(Card card in pcon.discardPile) {
            if(card.type == cardType) {
                card.restoreCost();
            }
        }
    }
}
