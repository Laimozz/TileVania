using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    bool wasCollected = false;
    [SerializeField] int coinScore = 100;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(coinScore);
            //GetComponent<AudioSource>().Play();  
            AudioSource.PlayClipAtPoint(coinSFX , Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
