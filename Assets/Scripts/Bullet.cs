using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float xSpeed;
    Rigidbody2D myRigidbody;
    [SerializeField] PlayerMovement playerMovement;
    void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        xSpeed = playerMovement.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity =  new Vector2 (xSpeed, 0);
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy (other.gameObject);
        }
        Destroy (transform.gameObject);
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(transform.gameObject);  
    }
}
