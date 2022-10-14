using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //Textures to be rendered
    private Sprite darkSprite;
    private Sprite lightSprite;

    //Positions of player and monster
    private Vector2 PlayerPosition;
    private Vector2 StartingPosition;

    //Detection Radius
    private int DetectionRadius;

    //Speed and Patrol state
    private float Speed = 0.1f; //ADJUST BASED ON SIZE OF GAME
    private bool isPatrolling;
    public int PatrolRadius;
    private bool isLeftFacing;
    private bool isIlluminated;

    // Start is called before the first frame update
    void Start()
    {
        //Assign starting point for the monster to return
        StartingPosition = this.transform.position;
        isPatrolling = true;
        isIlluminated = false;
        isLeftFacing = true;
        DetectionRadius = 100;
        PatrolRadius = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //Search for the player
        PlayerPosition = GameObject.Find("Player").transform.position;
        ChangeSprite();

        //If illuminated, allows movement
        if (!isIlluminated)
        {
            if ((this.transform.position.x <= PlayerPosition.x + DetectionRadius && this.transform.position.y <= PlayerPosition.y + DetectionRadius) &&
                (this.transform.position.x >= PlayerPosition.x - DetectionRadius && this.transform.position.y >= PlayerPosition.y - DetectionRadius))
            {
                isPatrolling = false;
                Attack();
            }
            else
            {
                //Patrol if at starting position
                if (isPatrolling)
                {
                    Patrol();
                }
                //Return to starting position
                else
                {
                    this.transform.position = Vector2.MoveTowards(transform.position, StartingPosition, Speed);
                    if ((Vector2)this.transform.position == StartingPosition)
                    {
                        isPatrolling = true;
                    }
                }
            }
        }
    }

    //Move close to the player to attack them
    void Attack()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Speed);
    }

    // Move along set routes : TO BE IMPLEMENTED AT A LATER PROTOTYPE
    void Patrol()
    {
        Vector2 NewLocation = StartingPosition;
        if( isLeftFacing )
        {
            if (this.transform.position.x < StartingPosition.x + PatrolRadius)
            {
                NewLocation.x = StartingPosition.x + 50;
                this.transform.position = Vector2.MoveTowards(transform.position, NewLocation, Speed);
            }
            else
            {
                isLeftFacing = false;
            }
        }
        else
        {
            if (this.transform.position.x > StartingPosition.x - PatrolRadius)
            {
                NewLocation.x = StartingPosition.x - 50;
                this.transform.position = Vector2.MoveTowards(transform.position, NewLocation, Speed);
            }
            else
            {
                isLeftFacing = true;
            }
        }
    }

    //Changes sprites based on light level
    void ChangeSprite()
    {
        if( isIlluminated )
        {
            //this.GetComponent<SpriteRenderer>().sprite = lightSprite;
        }
        else
        {
            //this.GetComponent<SpriteRenderer>().sprite = darkSprite;
        }
    }
}
