using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool destroySelfOnCollision = false;
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
        if(destroySelfOnCollision) {
            Destroy(gameObject);
        }
        GameObject battle = GameObject.FindGameObjectWithTag("Battle");
        battle.SendMessage("UpdateUI");
    }
}
