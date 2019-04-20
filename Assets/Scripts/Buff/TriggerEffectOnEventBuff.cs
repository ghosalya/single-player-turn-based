using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buff/TriggerEffectOnEvent")]
public class TriggerEffectOnEventBuff : Buff
{
    public string triggeringEvent;
    public Effect[] effects;

    public override void OnEvent(string eventID) {
        if(eventID == triggeringEvent) {
            foreach(Effect effect in effects) {
                effect.activate();
            }
        }
    }
}
