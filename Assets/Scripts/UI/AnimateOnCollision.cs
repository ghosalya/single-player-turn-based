using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool destroySelfOnCollision = false;
    public UnitBehaviour.UnitAffiliation target = UnitBehaviour.UnitAffiliation.ENEMY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SendMessage("UpdateUI");
        Debug.Log("Projectile hit");
        UnitBehaviour unit = other.GetComponent<UnitBehaviour>();
        if(destroySelfOnCollision && unit.affiliation == target) {
            playHitSFX();
            Destroy(gameObject);
        }
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        battle.SendMessage("UpdateUI");
    }

    void playHitSFX() {
        GameObject hitSFX = GameObject.Find("HitSFX");
        if (hitSFX != null) {
            hitSFX.GetComponent<AudioSource>().Play();
        }
    }
}
