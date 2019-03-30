using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Card")]
public class Card : ScriptableObject
{
    public enum CardType
    {
        ATTACK,
        DEFENSE,
        POWER,
        ULTIMATE
    }

    public string cardName;
    public string description;
    public int cost;
    public bool needTarget;
    public CardType type;
    public List<Effect> effects;

    public void OnEnable() {
        foreach(Effect effect in effects) {
            effect.card = this;
        }
    }

    public Card clone() {
        List<Effect> effectsClone = new List<Effect>();
        foreach(Effect effect in effects) {
            effectsClone.Add(Instantiate(effect));
        }
        Card cardClone = Instantiate(this);
        cardClone.effects = effectsClone;
        cardClone.OnEnable();
        return cardClone;
    }
}
