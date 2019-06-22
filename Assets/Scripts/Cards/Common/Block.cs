using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Block : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Block";
        card.description = "Block 30 damage from a lane.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 30;
        card.effects = new List<Effect>(){
            new BlockEffect(card, 30),
        };
        return card;
    }
}

public class BlockEffect : Effect
{
    int blockGain;
    public BlockEffect(Card card, int blockGain) : base(card) {
        this.blockGain = blockGain;
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();


        int column = pcon.cellSelected[0];
        int modifiedBlockGain = pcon.getModifiedBlock(this.blockGain);
        pcon.block[column] += modifiedBlockGain;

    }
}
