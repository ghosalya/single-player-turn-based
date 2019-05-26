using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Buff buff;

    public Text buffName;
    public Text buffDetail;
    public Image buffDetailPanel;

    void Update() {
        updateUI();
    }

    void updateUI() {
        if(buff == null) {
            buffName.text = "ERR";
            buffDetail.text = "ERR";
        } else {
            buffName.text = buff.displayName + " (" + buff.stack.ToString() + ")";
            buffDetail.text = buff.description;
        }
    }

    public void OnPointerEnter(PointerEventData ped) {
        buffDetailPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData ped) {
        buffDetailPanel.gameObject.SetActive(false);
    }

    public void applyBuff(Buff buffToApply) {
        this.buff = buffToApply;
        updateUI();
    }
}
