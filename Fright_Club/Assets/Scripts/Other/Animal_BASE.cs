using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private float HiddenSpeed;
    private float CloseToPlayerSpeed;
    private int PlayerDetectionRadius;
    private bool Frienzied;

    //Player and starting position
    private Vector2 PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = GameObject.Find("Player").transform.position;

        HiddenSpeed = 1.0f;
        CloseToPlayerSpeed = 3.5f;

        PlayerDetectionRadius = 20;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ( DetectPlayer && !Frienzied )
        {
            Frienzied = true;
        }
        else if ( Frienzied )
        {
            FleePlayer();
        }
        else
        {

        }
        */
    }

    /*
    //Checks if the player is close to the animal
    bool DetectPlayer()
    {
        if (this.transform.position.x <= PlayerPosition.x + PlayerDetectionRadius && this.transform.position.y <= PlayerPosition.y + PlayerDetectionRadius &&
             this.transform.position.x >= PlayerPosition.x - PlayerDetectionRadius && this.transform.position.y >= PlayerPosition.y - PlayerDetectionRadius)
            return true;
        else
            return false;
    }
    */

    /*
    //Makes the animal run away from the player
    void FleePlayer()
    {
        this.transform.LookAt();
        this.transform.Rotate(0, 180, 0);
        this.transform.Translate(Vector2.forward);
    }
    */
    //Has the animal move in a random direction
    void ChooseMovement()
    {

    }

    void OnCollisionEnter2D( Collision2D objectColliding )
    {
        Destroy(this);
    }
}
