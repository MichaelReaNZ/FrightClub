using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //Textures, Animations and Illumination status
    public Sprite darkSprite;
    public Animator darkAnimation;
    public Sprite lightSprite;
    public bool isIlluminated;

    //Positions of player and monster
    private Vector2 PlayerPosition;
    private Vector2 StartingPosition;

    //Patrol Values
    public Vector2 firstMark;
    public Vector2 secondMark;
    public Vector2 thirdMark;
    public Vector2 fourthMark;
    private Vector2[] patrolMarks;
    private int currentMark;

    //Speed and Patrol state
    public float Speed = 0.15f; //ADJUST BASED ON SIZE OF GAME
    private bool isPatrolling;

    //Detection Radius
    public int PlayerDetectionRadius;
    public int PlayerTrackingRadius;


    // Start is called before the first frame update
    void Start()
    {
        //Assign starting point for the monster to return
        StartingPosition = this.transform.position;
        isPatrolling = true;
        isIlluminated = false;

        //Radiuses for Player detection and tracking
        PlayerDetectionRadius = 100;
        PlayerTrackingRadius = 75;

        //Patrol Values setup
        patrolMarks = new Vector2[4]
        {
            firstMark,
            secondMark,
            thirdMark,
            fourthMark
        };
        currentMark = 0;
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
            if ((this.transform.position.x <= PlayerPosition.x + PlayerDetectionRadius && this.transform.position.y <= PlayerPosition.y + PlayerDetectionRadius) &&
                (this.transform.position.x >= PlayerPosition.x - PlayerDetectionRadius && this.transform.position.y >= PlayerPosition.y - PlayerDetectionRadius) &&
                (this.transform.position.x <= StartingPosition.x + PlayerTrackingRadius && this.transform.position.y <= StartingPosition.y + PlayerTrackingRadius) &&
                (this.transform.position.x >= StartingPosition.x - PlayerTrackingRadius && this.transform.position.y >= StartingPosition.y - PlayerTrackingRadius))
            {
                isPatrolling = false;
                currentMark = 0;
                Attack();
            }
            else
            {
                //Patrol if at starting position
                if (isPatrolling)
                {
                    currentMark = Patrol(currentMark);
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

    // Move along set routes, points must be implemented into the editor on creation
    int Patrol( int pointToMove )
    {
        this.transform.position = Vector2.MoveTowards(transform.position, patrolMarks[pointToMove], Speed);
        if( this.transform.position.x == patrolMarks[pointToMove].x && this.transform.position.y == patrolMarks[pointToMove].y )
        {
            pointToMove++;
            if( pointToMove == patrolMarks.Length )
            {
                pointToMove = 0;
            }
        }
        return pointToMove;
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
            /*
                this.GetComponent<SpriteRenderer>().sprite = darkSprite;
                if(!darkAnimation.Play)
                {
                    darkAnimation.Play("Monster_Move");
                }
            */
        }
    }
}
