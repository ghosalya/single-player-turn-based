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
    public Card cardPlayed = null;
    public GameObject selectionCircle;

    public List<Card> drawPile;
    public List<Card> handCards;
    public List<Card> discardPile;

    // Start is called before the first frame update
    void Start()
    {
        // temporary
        health = 500;
        energy = 100;

        cardPlayed = null;
        cellSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        executePlayedCard();
    }

    public void OnTurnStart()
    {
        draw(5);
    }

    public void OnTurnEnd() {
        energy = Mathf.Clamp(energy + 100, 0, 150);
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
        return card.cost <= energy;
    }

    public void selectCardAsPlayed(Card card) {
        cardPlayed = card;
        if(card.needTarget) {
            selectionCircle.SetActive(true);
        }
    }

    public void executePlayedCard() {
        if(cardPlayed != null) {
            if(cardPlayed.needTarget == false || cellSelected != null) {
                energy -= cardPlayed.cost;
                foreach(Effect effect in cardPlayed.effects)
                {
                    effect.activate();
                }

                // after paying, reset controller states
                cardPlayed = null;
                cellSelected = null;
            }
        }
        // else pass
    }

    public void play(Card card)
    {
        if(canPlay(card))
        {
            selectCardAsPlayed(card);
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
