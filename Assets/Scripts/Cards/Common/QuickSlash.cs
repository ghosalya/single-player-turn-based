using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class QuickSlash: ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "QuickSlash";
        card.description = "Deals 30 damage to an enemy in Row 1.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 10;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 30, 0, 1),
        };
        return card;
    }
}