using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BananaPeelScript : MonoBehaviour
{

    public GameObject player;

    public float damage = 90;
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, 0f ,player.transform.position.z);
        Collider collider = GetComponent<Collider>();
        Collider playerCollider = player.GetComponent<Collider>();
        Physics.IgnoreCollision(collider, playerCollider);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            MonkeyBehaviour monkey = collision.gameObject.GetComponent<MonkeyBehaviour>();
            monkey.loseHealth(damage);
            Destroy(gameObject);             
        }
    }
}
