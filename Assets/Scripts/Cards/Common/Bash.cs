using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Bash: ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Bash";
        card.description = "Deals 50 damage to the first unit in a lane and knock it back.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 40;
        card.effects = new List<Effect>(){
            new StrikeEffect(card, 50, 1, 5),
        };
        return card;
    }
}