using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanelUI : MonoBehaviour
{
    public PlayerController pcon;
    public GameObject buffUIPrefab;
    public List<GameObject> buffUIList;
    
    // Start is called before the first frame update
    void Start()
    {
        pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI() {
        foreach (GameObject buffUI in buffUIList) {
            Destroy(buffUI);
        }

        buffUIList = new List<GameObject>();

        for (int i = 0; i < pcon.buffs.Count; i++) {
            int x = 5 + (105 * (i % 3));
            int y = -5 - (30 * (i / 3));
            GameObject genBuffUI = Instantiate(buffUIPrefab, transform);
            genBuffUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            genBuffUI.GetComponent<BuffUI>().applyBuff(pcon.buffs[i]);
            buffUIList.Add(genBuffUI);
        }
    }
}
