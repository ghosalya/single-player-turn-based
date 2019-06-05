using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Strategy : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Strategy.";
        card.description = "Discard 1 random card. Draw 1 card.";
        card.type = Card.CardType.SKILL;
        card.needTarget = false;
        card.baseCost = 20;
        card.effects = new List<Effect>(){
            new DiscardRandomEffect(card),
            new DrawEffect(card, 1),
        };
        return card;
    }
}

public class DiscardRandomEffect: Effect {

    public DiscardRandomEffect(Card card) : base(card) {}
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        List<Card> discardable = new List<Card>(pcon.handCards);
        discardable.Remove(this.card);
        if (discardable.Count == 0) {
            Debug.Log(this.card.cardName + ": Discard nulled - no cards in hand to discard.");
            return;
        }
        Card toDiscard = discardable[Random.Range(0, discardable.Count)];
        pcon.discard(toDiscard);
    }
}
public class DrawEffect: Effect {

    int cardsToDraw;
    public DrawEffect(Card card, int cardsToDraw) : base(card) {
        this.cardsToDraw = cardsToDraw;
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        pcon.draw(cardsToDraw);
    }
}