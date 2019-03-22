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
    public GameObject cards;
    private Vector3[] cardPosition = {new Vector3(400f,80f,0f), new Vector3(325f,80f,0f), new Vector3(250f,80f,0f), new Vector3(175f,80f,0f), new Vector3(100f,80f,0f)};
    private Vector3 position;
    private GameObject currentCard;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        
        //If there are cards in the hand, spawn card prefab and give it the info from the list
        if(pcon.startOfTurn == true)
        {
            spawnCards(pcon.handCards.Count);
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

    void spawnCards(int numOfCard)
    {
        PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        GameObject panel = GameObject.Find("HandCardPanel");
        for(int i=0; i<numOfCard; i++)
        {
            position = cardPosition[i];
            currentCard = Instantiate(cards, position, this.transform.rotation) as GameObject;
            currentCard.GetComponent<HandCardUI>().card = pcon.handCards[i];
            currentCard.transform.parent = panel.transform;
        }
    }
}
