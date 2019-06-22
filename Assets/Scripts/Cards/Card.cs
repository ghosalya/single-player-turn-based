using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
    public Card card;

    public Effect(Card card) {
        this.card = card;
    }
    public abstract void activate();
}


public class Card
{
    public enum CardType
    {
        ATTACK,
        DEFENSE,
        SKILL,
        ULTIMATE
    }

    public Card() {
        this.cardName = "A Card";
        this.description = "Just a card..";
        this.type = CardType.DEFENSE;
        this.needTarget = false;
        this.baseCost = 1000;
        this.costMultiplier = 1;
        this.costBonus = 0;
        this.effects = new List<Effect>();
    }

    Card(Card original) {
        this.cardName = original.cardName;
        this.description = original.description;
        this.type = original.type;
        this.needTarget = original.needTarget;
        this.baseCost = original.baseCost;
        this.costMultiplier = original.costMultiplier;
        this.costBonus = original.costBonus;
        this.effects = original.effects;
    }

    public bool needTarget;
    public CardType type;

    public string cardName;
    public string description;

    public float costMultiplier;
    public float costBonus;
    public int baseCost;

    public List<Effect> effects;

    public void activate() {
        foreach (Effect effect in effects) {
            effect.activate();
        }
    }

    public int cost() {
        return Mathf.RoundToInt(this.baseCost * costMultiplier + costBonus);
    }

    public void restoreCost() {
        this.costMultiplier = 1;
        this.costBonus = 0;
    }

    public Card clone() {
        return new Card(this);
    }

}


public interface ICardFactory
{
    Card card();
}