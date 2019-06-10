using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Shield : ICardFactory
{
    public Card card() {
        Card card = new Card();
        card.cardName = "Shield";
        card.description = "Summon a 15 health shield, or give a summon 30 block.";
        card.type = Card.CardType.ATTACK;
        card.needTarget = true;
        card.baseCost = 30;
        card.effects = new List<Effect>(){
            new ShieldEffect(card, 15, 30),
        };
        return card;
    }
}

public class ShieldEffect : Effect
{
    int health, block;
    public ShieldEffect(Card card, int health, int block) : base(card) {
        this.health = health;
        this.block = block;
    }
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        EnemySquad esquad = battle.GetComponent<EnemySquad>();


        int column = pcon.cellSelected[0];
        GameObject existingSummon = pcon.getSummonAtFriendlyTile(column);
        if (existingSummon == null) {
            GameObject prefab = Resources.Load<GameObject>("Summon\\Common\\Shield");
            prefab.GetComponent<UnitHealth>().maxHealth = this.health;
            prefab.GetComponent<UnitHealth>().health = this.health;
            pcon.summon(prefab, column);
        } else {
            existingSummon.GetComponent<UnitHealth>().gainBlock(this.block);
        }
    }
}
