using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/ReshuffleAllCardOfType")]
public class ReshuffleAllCardOfType : Effect
{
    public Card.CardType type;
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();

        List<Card> toBeReshuffled = new List<Card>();
        foreach(Card card in pcon.discardPile) {
            if(card.type == type) {
                toBeReshuffled.Add(card);
            }
        }

        
        int cardsToShuffle = toBeReshuffled.Count;
        for(int i = 0; i < cardsToShuffle; i++)
        {
            int index = Random.Range(0, toBeReshuffled.Count);
            pcon.drawPile.Add(toBeReshuffled[index]);
            pcon.discardPile.Remove(toBeReshuffled[index]);
            toBeReshuffled.RemoveAt(index);
        }
    }
}
