using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    //destroy on collision
    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if(proj != null) { Destroy(other.gameObject); }
    }
}
