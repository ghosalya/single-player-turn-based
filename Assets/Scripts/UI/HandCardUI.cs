using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardUI : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerUI playerUI;
    public Card card;
    public Text cardNameText;
    public Text cardCostText;
    public Text cardDescriptionText;
    public GameObject selectedGlow;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        playerUI = GameObject.Find("BarsPanel").GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        cardNameText.text = card.cardName;
        cardCostText.text = "Cost:" + card.cost.ToString();
        cardDescriptionText.text = card.description;

        if(this == playerUI.cardPlayed) {
            selectedGlow.SetActive(true);
        } else {
            selectedGlow.SetActive(false);
        }
    }

    public void activate()
    {
        playerUI.play(this);
    }
}
