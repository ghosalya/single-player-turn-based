using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Effect/BonusBlockPerEnemyInColumn")]
public class BlockBonusPerEnemyInColumn : Effect
{
    public int initialBlock;
    public int bonusBlock;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];

        int blockgain = getTotalBlockGain(column);
        int modifiedBlockGain = pcon.getModifiedBlock(blockgain);
        pcon.block[column] += modifiedBlockGain;

    }

    public int getTotalBlockGain(int column) {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        EnemySquad esquad = battle.GetComponent<EnemySquad>();

        int blockgain = initialBlock;
        foreach(GameObject enemy in esquad.enemies) {
            GridPosition gpos = enemy.GetComponent<GridPosition>();
            if(gpos != null) {
                if (gpos.column == column) {
                    blockgain += bonusBlock;
                }
            }
        }
        return Mathf.Clamp(blockgain, 0, blockgain + 1);

    }
}
