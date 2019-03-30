using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Effect/BonusBlockPerCardPlayed")]
public class BlockBonusPerCardPlayed : Effect
{
    public int initialBlock;
    public int bonusBlock;
    public Card.CardType bonusCardType;
    public override void activate() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();
        int column = pcon.cellSelected[0];

        int blockgain = getTotalBlockGain();
        int modifiedBlockGain = pcon.getModifiedBlock(blockgain);
        pcon.block[column] += modifiedBlockGain;

    }

    public int getTotalBlockGain() {
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        PlayerController pcon = battle.GetComponent<PlayerController>();

        int blockgain = initialBlock;
        foreach(Card card in pcon.turnHistory) {
            if (card.type == bonusCardType) {
                blockgain += bonusBlock;
            }
        }
        return blockgain;

    }
}
