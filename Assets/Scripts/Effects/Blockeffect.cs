using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Block")]
public class Blockeffect : Effect
{
    public int blockgain;
    // E
    public override void activate()
    {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");

        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];
        pcon.block[column] += blockgain;






           }

    // Update is called once per frame
    void Update()
    {
        
    }
}
