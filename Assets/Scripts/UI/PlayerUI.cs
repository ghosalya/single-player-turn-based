using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public RawImage healthbar;
    public Text healthtext;
    public RawImage energyBar;
    public Text energyText;
    public DamageRedScreen damageRedScreen;
    public GameObject cards;
    private Vector3 cardPosition = new Vector3(380f,80f,0f);
    private Vector3 position;
    private GameObject currentCard;
    public HandCardUI cardPlayed = null;
    public GameObject selectionCircle;
    public PlayerController pcon;
    public List<GameObject> cardsInHand;
    // Start is called before the first frame update
    void Start()
    {
        pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If there are cards in the hand, spawn card prefab and give it the info from the list
        if(pcon.startOfTurn == true)
        {
            spawnCards(pcon.handCards);
            pcon.startOfTurn = false;
        }

        //Player's Health
        healthtext.text = pcon.health.ToString();
        float healthwidth = 200 * pcon.health / pcon.maxHealth;
        healthbar.rectTransform.sizeDelta = new Vector2(healthwidth, 15);

        //Player's Energy
        energyText.text = pcon.energy.ToString();
        float energywidth = 200 * pcon.energy / 150;
        energyBar.rectTransform.sizeDelta = new Vector2(energywidth, 15);
    }

    void spawnCards(List<Card> toBeSpawned)
    {
        GameObject panel = GameObject.Find("HandCardPanel");
        for(int i=0; i<toBeSpawned.Count; i++)
        {
            // Debug.Log("spawned 1 card");
            position = cardPosition - new Vector3(cardsInHand.Count * 80, 0, 0);
            currentCard = Instantiate(cards, position, this.transform.rotation) as GameObject;
            currentCard.GetComponent<HandCardUI>().card = pcon.handCards[i];
            currentCard.transform.SetParent(panel.transform);
            cardsInHand.Add(currentCard);
        }
    }

    public void destroyCardUI(HandCardUI toBeDestroyed)
    {
        Destroy(toBeDestroyed.gameObject);
    }

    public void emptyHand(List<GameObject> toBeDestroyed)
    {
        int size;
        size = toBeDestroyed.Count;
        for(int i=0; i<size;i++)
        {
            GameObject a = toBeDestroyed[0];
            toBeDestroyed.Remove(a);
            Destroy(a);
        }
    }

    public void refreshHand() {
        emptyHand(cardsInHand);
        spawnCards(pcon.handCards);
    }

    public void selectCardAsPlayed(HandCardUI card) {
        cardPlayed = card;
        if(cardPlayed.card.needTarget) {
            selectionCircle.SetActive(true);
        }
    }

    public void play(HandCardUI cardUI)
    {
        if(pcon.canPlay(cardUI.card))
        {
            selectCardAsPlayed(cardUI);
        } else
        {
            Debug.LogError("Card Unplayable: " + cardUI.card.cardName);
        }
    }

    public void onTakingDamage(int damage) {
        damageRedScreen.flashOnDamage(damage);
    }


}
