using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Hyperblast: ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Hyperblast";
        card.description = "Deals 200 damage to the first unit in a lane.";
        card.type = Card.CardType.ULTIMATE;
        card.needTarget = true;
        card.baseCost = 90;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 200, 0, 5),
        };
        return card;
    }
}