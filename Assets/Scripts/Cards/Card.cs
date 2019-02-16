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
    public CardType type;
    public Effect[] effects;
}
