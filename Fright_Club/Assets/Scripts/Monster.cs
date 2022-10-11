using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Vector2 PlayerPosition;
    private Vector2 StartingPosition;
    private float Speed = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        //Assign starting point for the monster to return
        StartingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Search for the player and move towards them if they are close
        PlayerPosition = GameObject.Find("Player").transform.position;

        if( (gameObject.transform.position.x >= PlayerPosition.x - 5 && gameObject.transform.position.y >= PlayerPosition.y - 5 ) ||
           (gameObject.transform.position.x <= PlayerPosition.x + 5 && gameObject.transform.position.y <= PlayerPosition.y + 5) )
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Speed);
        }
        else
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, StartingPosition, Speed);
        }
    }

    // Move along set routes
    void Patrol()
    {

    }
}
