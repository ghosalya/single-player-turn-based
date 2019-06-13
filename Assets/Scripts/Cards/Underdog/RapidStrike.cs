using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class RapidStrike : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Rapid Strike";
        card.description = "Deals 20 damage to the first unit in a lane 3 times.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 35;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 20, 0, 5),
            new StrikeEffect(card, 20, 0, 5),
            new StrikeEffect(card, 20, 0, 5),
        };
        return card;
    }
}
