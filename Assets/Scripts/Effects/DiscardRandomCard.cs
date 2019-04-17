using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DiscardRandomCard")]
public class DiscardRandomCard : Effect
{
    public int cardsDiscarded;
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        for(int i = 0; i < cardsDiscarded; i++) {
            List<Card> discardable = new List<Card>();
            foreach(Card card in pcon.handCards) {
                if(card != this.card) {
                    discardable.Add(card);
                }
            }
            if(discardable.Count == 0) {
                Debug.Log(this.name + ": Nulled - no cards in hand to discard.");
                return;
            }
            Card toDiscard = discardable[Random.Range(0, discardable.Count)];
            pcon.discard(toDiscard);
        }
    }
}
