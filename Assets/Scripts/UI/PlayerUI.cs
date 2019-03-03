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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        
        healthtext.text = pcon.health.ToString();
        float healthwidth = 200 * pcon.health / pcon.maxHealth;
        healthbar.rectTransform.sizeDelta = new Vector2(healthwidth, 15);

        energyText.text = pcon.energy.ToString();
        float energywidth = 200 * pcon.energy / 150;
        energyBar.rectTransform.sizeDelta = new Vector2(energywidth, 15);
    }
}
