using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Summon")]
public class Summon : Effect
{
    // Start is called before the first frame update
    public GameObject famil;
    public override void activate()

    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];
        if (column==0)
            {
            Instantiate(famil, new Vector3(-13.74f, 1.05f, -21.91f), famil.transform.rotation);
            }
        if (column == 1)
            {
            Instantiate(famil, new Vector3(-4.93f, 1.05f, -21.91f), famil.transform.rotation);
            }
        if (column == 2)
        {
            Instantiate(famil, new Vector3(4.24f, 1.05f, -21.91f), famil.transform.rotation);
        }
        if (column == 3)
        {
            Instantiate(famil, new Vector3(12.97f, 1.05f, -21.91f), famil.transform.rotation);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
