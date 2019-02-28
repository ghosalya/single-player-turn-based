using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardUI : MonoBehaviour
{
    public PlayerController playerController;
    public Card card;
    public Text cardNameText;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        cardNameText.text = card.cardName;
    }

    public void activate()
    {
        playerController.play(card);
    }
}
