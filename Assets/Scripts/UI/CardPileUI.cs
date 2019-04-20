using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPileUI : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerController.CardPile pile;
    public Button toggleButton;
    public GameObject panel;
    public RectTransform viewportContent;
    public GameObject cardPrefab;
    List<GameObject> currentlyViewedCards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        toggleButton.onClick.AddListener(toggle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toggle() {
        refreshCardsInPile();
        panel.SetActive(!panel.activeSelf);
    }

    void refreshCardsInPile() {
        List<Card> cardsToView = playerController.getPile(pile);
        destroyCurrentlyViewed();
        currentlyViewedCards = new List<GameObject>();
        for (int i = 0; i < cardsToView.Count; i++) {
            int x = 10 + (90 * (i % 5));
            int y = -10 - (110 * (i / 5));
            GameObject newCardview = Instantiate(cardPrefab, viewportContent);
            newCardview.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            newCardview.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            newCardview.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            newCardview.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            newCardview.GetComponent<Button>().enabled = false;
            newCardview.GetComponent<HandCardUI>().card = cardsToView[i];
            currentlyViewedCards.Add(newCardview);
        }

        // resize viewport
        int height = (cardsToView.Count / 5) * 110;
        // panel.GetComponent<RectTransform>().sizeDelta = new Vector2(475, Mathf.Clamp(height, 0, 275));
        viewportContent.sizeDelta = new Vector2(viewportContent.sizeDelta.x,  height);

    }

    void destroyCurrentlyViewed() {
        foreach (GameObject cardView in currentlyViewedCards) {
            Destroy(cardView);
        }
    }
}
