using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 500;
    public int health { get; private set; }
    public int energy { get; private set; }
    public int[] block = new int[4];

    public int[] cellSelected = null;

    public List<Card> drawPile;
    public List<Card> handCards;
    public List<Card> discardPile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTurnStart()
    {
        draw(5);
    }

    public void draw(int drawCount)
    {
        for(int i = 0; i < drawCount; i++)
        {
            Card drawnCard = drawPile[0];
            drawPile.Remove(drawnCard);
            handCards.Add(drawnCard);
        }
    }

    public bool canPlay(Card card)
    {
        return card.cost < energy;
    }

    public void play(Card card)
    {
        if(canPlay(card))
        {
            energy -= card.cost;
            foreach(Effect effect in card.effects)
            {
                effect.activate();
            }
        } else
        {
            Debug.LogError("Card Unplayable: " + card.cardName);
        }
    }

    public void discard(Card card)
    {
        if (handCards.Contains(card))
        {
            handCards.Remove(card);
            discardPile.Add(card);
        } else {
            Debug.LogError("Trying to discard a card that's not in the hand: " + card.cardName);
        }
    }

    public void reshuffle()
    {
        // shuffling
        int cardsToShuffle = discardPile.Count;
        for(int i = 0; i < cardsToShuffle; i++)
        {
            int index = Random.Range(0, discardPile.Count);
            drawPile.Add(drawPile[index]);
            discardPile.RemoveAt(index);
        }
    }

    public void takeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
    }
}
