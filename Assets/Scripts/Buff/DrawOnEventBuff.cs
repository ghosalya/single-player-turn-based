using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buff/DrawOnEvent")]
public class DrawOnEventBuff : Buff
{
    public string triggeringEvent;
    public int eventCountToTrigger;
    int triggerCount = 0;

    public override void OnEvent(string eventID) {
        if(eventID == triggeringEvent) {
            triggerCount += 1;
            if(triggerCount >= eventCountToTrigger) {
                GameObject battle = GameObject.FindGameObjectWithTag("Battle");
                PlayerController pcon = battle.GetComponent<PlayerController>();
                pcon.draw(1);
                Debug.Log("Draw one card from buff: " + this.name);
                triggerCount = 0;
            }
        }
    }
}
