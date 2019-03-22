using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 500;
    public int health { get; private set; }
    public int energy { get; private set; }
    public int[] block = new int[4];
    public GameObject familiar;
    public Transform famil;

    public int[] cellSelected = null;
    public Card cardPlayed = null;
    public GameObject selectionCircle;

    public List<Card> drawPile;
    public List<Card> handCards;
    public List<Card> discardPile;
    public bool startOfTurn = false;

    public List<Buff> buffs;

    // Start is called before the first frame update
    void Start()
    {
        // temporary
        health = 500;
        energy = 100;

        cardPlayed = null;
        cellSelected = null;

        buffs = new List<Buff>();
    }

    // Update is called once per frame
    void Update()
    {
        executePlayedCard();

    }

    public void OnTurnStart()
    {
        draw(5);
        startOfTurn = true;
        FeedEventToBuffs("OnTurnStart");
    }

    public void FeedEventToBuffs(string eventID)
    {
        foreach(Buff buff in buffs) {
            buff.OnEvent(eventID);
        }
    }

    public void OnTurnEnd() {
        energy = Mathf.Clamp(energy + 100, 0, 150);

        // buff handling
        List<Buff> buffnext = new List<Buff>();
        foreach(Buff buff in buffs) {
            buff.ConsumeStackOnTurnEnd();
            if(buff.stack > 0) {buffnext.Add(buff);}
        }
        buffs = buffnext;

        FeedEventToBuffs("OnTurnEnd");
    }

    public void draw(int drawCount)
    {
        for(int i = 0; i < drawCount; i++)
        {
            Card drawnCard = drawPile[0];
            drawPile.Remove(drawnCard);
            handCards.Add(drawnCard);
        }
        FeedEventToBuffs("OnDraw");
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
                    FeedEventToBuffs("OnCardPlay");
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

    public void takeDamage(int damage, int column)
    {

        int damageTaken = damage - block[column];
        if (damageTaken > 0) {
            block[column] = 0;
            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);
        } else {
            block[column] -= damage;
        }
    }

    public int getModifiedDamage(int originalDamage)
    {
        int finalDamage = originalDamage;
        foreach (Buff buff in buffs) {
            finalDamage = buff.MultiplyDamage(finalDamage);
        }
        foreach (Buff buff in buffs) {
            finalDamage = buff.AddBonusDamage(finalDamage);
        }
        return finalDamage;
    }

    public int getModifiedBlock(int originalBlockGain)
    {
        int finalBlockGain = originalBlockGain;
        foreach (Buff buff in buffs) {
            finalBlockGain = buff.MultiplyBlock(finalBlockGain);
        }
        foreach (Buff buff in buffs) {
            finalBlockGain = buff.AddBonusBlock(finalBlockGain);
        }
        return finalBlockGain;
    }
}
