using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Sidestep : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Sidestep";
        card.description = "Gain 25 block in both inner lanes.";
        card.type = Card.CardType.DEFENSE;
        card.needTarget = false;
        card.baseCost = 30;
        card.effects = new List<Effect>(){
            new FixedPositionBlockEffect(card, 25, 1),
            new FixedPositionBlockEffect(card, 25, 2),
        };
        return card;
    }
}
public class FixedPositionBlockEffect : Effect
{
    int blockGain, column;
    public FixedPositionBlockEffect(Card card, int blockGain, int column) : base(card) {
        this.blockGain = blockGain;
        this.column = column;
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();

        int modifiedBlockGain = pcon.getModifiedBlock(this.blockGain);
        pcon.block[column] += modifiedBlockGain;

    }
}