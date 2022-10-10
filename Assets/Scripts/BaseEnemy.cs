using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    private Vector2 PlayerPosition;
    private int Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Assign Player position to Player
    }

    // Update is called once per frame
    void Update()
    {
        //If( Monster is near player )
        gameObject.transform.position = Vector2.MoveTowards( transform.position, PlayerPosition, Speed );
    }

    // Move along set routes
    void Patrol()
    {

    }
}
