using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 1);
    public float speed = 50;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //destroy on collision
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
