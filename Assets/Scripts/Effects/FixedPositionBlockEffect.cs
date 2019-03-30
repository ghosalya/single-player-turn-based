using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Effect/FixedPositionBlock")]
public class FixedPositionBlockEffect : Effect
{
    public int[] blockgain;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        for(int i = 0; i < blockgain.Length; i++) {
            pcon.block[i] += pcon.getModifiedBlock(blockgain[i]);
        }
    }
}
