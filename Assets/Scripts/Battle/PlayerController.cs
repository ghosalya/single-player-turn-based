using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 500;
    public int maxEnergy = 150;
    public int health { get; private set; }
    public int energy { get; private set; }
    public int[] block = new int[4];
    public List<GameObject> summons = new List<GameObject>();

    public int[] cellSelected = null;
    //public GameObject selectionCircle;

    public List<Card> deck = new List<Card>();

    public List<Card> drawPile = new List<Card>();
    public List<Card> handCards = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public bool startOfTurn = false;
    public PlayerUI playerUI;

    public List<Card> battleHistory = new List<Card>();
    public List<Card> turnHistory = new List<Card>();

    public List<Buff> buffs;

    public enum CardPile
    {
        HandCard,
        Discard,
        Draw
    }

    void Awake() {
        // TODO: change this to OnBattleStart
        constructDeck();
        initializeDrawPile();
        buffs = new List<Buff>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // temporary
        health = 500;
        energy = 150;
        playerUI = GameObject.Find("BarsPanel").GetComponent<PlayerUI>();

        cellSelected = null;
    }

    private void constructDeck() {
        deck.Add(new Strike().card());
        deck.Add(new Strike().card());
        deck.Add(new Shield().card());
        deck.Add(new Strike().card());
        deck.Add(new Strike().card());
        deck.Add(new Shield().card());
        deck.Add(new QuickSlash().card());
        deck.Add(new QuickSlash().card());
        deck.Add(new Bash().card());
        deck.Add(new Shield().card());
        deck.Add(new Strategy().card());
        deck.Add(new Hyperblast().card());
        deck.Add(new Block().card());
        deck.Add(new Block().card());
        deck.Add(new Block().card());
    }

    // Update is called once per frame
    void Update()
    {
        executePlayedCard();

    }

    public List<Card> getPile(CardPile pile) {
        if (pile == CardPile.Discard) {
            return discardPile;
        } else if (pile == CardPile.Draw) {
            return drawPile;
        } else if (pile == CardPile.HandCard) {
            return handCards;
        } else {
            return null;
        }
    }

    public void OnTurnStart()
    {
        startOfTurn = true;
        FeedEventToBuffs("OnTurnStart");
        draw(5);
        playerUI.refreshPlayerUI();
    }

    public void FeedEventToBuffs(string eventID)
    {
        foreach(Buff buff in buffs) {
            buff.OnEvent(eventID);
        }
    }

    public void OnTurnEnd() {
        energy = Mathf.Clamp(energy + 100, 0, 150);

        // history handling
        foreach(Card card in turnHistory) {
            battleHistory.Add(card);
        }
        turnHistory.Clear();
        foreach(Card card in battleHistory) {
            Debug.Log("Played - " + card.cardName);
        }

        // buff handling
        List<Buff> buffnext = new List<Buff>();
        foreach(Buff buff in buffs) {
            buff.ConsumeStackOnTurnEnd();
            if(buff.stack > 0) {
                buffnext.Add(buff);
            } else {
                buff.OnRemoved();
            }
        }
        buffs = buffnext;
        int toBeDiscarded = handCards.Count;
        for(int i=0;i<toBeDiscarded;i++)
        {
            discard(handCards[0]);
        }
        playerUI.emptyHand(playerUI.cardsInHand);
        FeedEventToBuffs("OnTurnEnd");
    }

    public void draw(int drawCount)
    {
        if(drawPile.Count < drawCount)
        {
            reshuffle();
        }
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
        return card.cost() <= energy;
    }

    /*
    public void selectCardAsPlayed(Card card) {
        cardPlayed = card;
        if(card.needTarget) {
            selectionCircle.SetActive(true);
        }
    }
    */

    public void executePlayedCard() {
        if(playerUI.cardPlayed != null) {
            Card card = playerUI.cardPlayed.card;
            if(card.needTarget == false || cellSelected != null) {
                energy -= card.cost();
                Debug.Log("Card played: " + card.cardName);
                card.activate();
                Debug.Log("Card finished playing: " + card.cardName);
                FeedEventToBuffs("OnCardPlay");
                turnHistory.Add(card);
                // after playing, reset controller states
                // playerUI.destroyCardUI(playerUI.cardPlayed);
                discard(card);
                playerUI.refreshPlayerUI();
                playerUI.cardPlayed = null;
                cellSelected = null;
                
                // Uncomment this for debugging
                // GetComponent<EnemySquad>().UpdateUI();
            }
        }
        // else pass
    }

    public void discard(Card card)
    {
        if (handCards.Contains(card))
        {
            handCards.Remove(card);
            discardPile.Add(card);
            FeedEventToBuffs("OnDiscard");
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
            drawPile.Add(discardPile[index]);
            discardPile.RemoveAt(index);
        }
        FeedEventToBuffs("OnReshuffle");
    }

    public void initializeDrawPile() {
        List<Card> toShuffle = new List<Card>();
        foreach(Card card in deck) {
            toShuffle.Add(card.clone());
        }
        int cardCount = toShuffle.Count;

        drawPile = new List<Card>();
        for (int i =0; i < cardCount; i++) {
            Card card = toShuffle[Random.Range(0, toShuffle.Count)];
            drawPile.Add(card);
            toShuffle.Remove(card);
        }
    }

    public void takeDamage(int damage, int column) {
        // damage is first reduced by block
        int damageTaken = damage - block[column];
        if (damageTaken < 0) {
            block[column] -= damage;
            return;
        } else {
            block[column] = 0;
        }

        GameObject damagedSummon = null;
        foreach(GameObject summon in this.summons) {
            if (summon.GetComponent<GridPosition>().column == column) {
                if (damagedSummon == null) { damagedSummon = summon; }
            }
        }

        // after block reduction, damage is either taken by player,
        // or taken by the summon (in which case it doesn't spill to player)
        if (damagedSummon != null) { takeSummonDamage(damageTaken, damagedSummon); }
        else { takePlayerDamage(damageTaken, column); }
    }

    void takePlayerDamage(int damage, int column)
    {

        int damageTaken = damage - block[column];
        if (damageTaken > 0) {
            block[column] = 0;
            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);
            playerUI.onTakingDamage(damageTaken);
        } else {
            block[column] -= damage;
        }
    }

    void takeSummonDamage(int damage, GameObject prefab) {
        prefab.GetComponent<UnitHealth>().takeDamage(damage);
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

    public void OnKill() {
        FeedEventToBuffs("OnKill");
    }

    public void OnGainEnergy() {
        FeedEventToBuffs("OnGainEnergy");
    }

    public void gainEnergy(int energyGain) {
        energy = Mathf.Clamp(energy + energyGain, 0, maxEnergy);
        SendMessage("OnGainEnergy");
    }

    public void getBuff(Buff addedBuff) {
        foreach (Buff curretBuff in buffs) {
            if (curretBuff.displayName == addedBuff.displayName) {
                curretBuff.stack += addedBuff.stack;
                return;
            }
        }
        // if no same buff found
        buffs.Add(addedBuff.clone());
        addedBuff.OnApplied();
    }

    public void summon(GameObject prefab, int column) {
        GameObject summoned = Instantiate(prefab, new Vector3(0, 0, -40), Quaternion.identity);
        summoned.GetComponent<GridPosition>().row = 0;
        summoned.GetComponent<GridPosition>().column = column;
        this.summons.Add(summoned);
    }

    public GameObject getSummonAtFriendlyTile(int column) {
        foreach(GameObject summoned in this.summons) {
            if (summoned.GetComponent<GridPosition>().row == 0) {
                if (summoned.GetComponent<GridPosition>().column == column) {
                    return summoned;
                }
            }
        }
        return null;
    }
}
