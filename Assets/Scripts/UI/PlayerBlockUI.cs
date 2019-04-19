using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] blockShield;
    public TextMesh[] blockAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateBlock();
    }

    void updateBlock() {
        PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        for(int i = 0; i < 4; i++) {
            if(pcon.block[i] <= 0) {
                blockShield[i].SetActive(false);
            } else {
                blockShield[i].SetActive(true);
            }
            blockAmount[i].text = pcon.block[i].ToString();
        }

    }
}
