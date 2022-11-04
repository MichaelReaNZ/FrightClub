using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource collectablePickUp;

    // Start is called before the first frame update
    void Start()
    {
        collectablePickUp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys this on collision with the player
    private void OnCollisionEnter2D( Collision2D objectColliding )
    {
        if ( objectColliding.gameObject.CompareTag("Player") )
        {
            collectablePickUp.Play();
            Destroy(this.gameObject);
        }
    }
}
