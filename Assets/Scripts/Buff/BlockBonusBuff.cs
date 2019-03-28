using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buff/BlockBonus")]
public class BlockBonusBuff : Buff
{
    public int blockBonus = 0;
    public float blockMultiplier = 1;

    public override int AddBonusBlock(int block) {
        return blockBonus + block;
    }

    public override int MultiplyBlock(int block) {
        return Mathf.RoundToInt(blockMultiplier * block);
    }
}
