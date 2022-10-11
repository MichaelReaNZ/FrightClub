using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //Textures to be rendered
    public Sprite darkSprite;
    public Sprite lightSprite;

    //Positions of player and monster
    private Vector2 PlayerPosition;
    private Vector2 StartingPosition;

    //Speed and Patrol state
    private float Speed = 0.002f; //ADJUST BASED ON SIZE OF GAME
    private bool isPatrolling;
    private bool isIlluminated;

    // Start is called before the first frame update
    void Start()
    {
        //Assign starting point for the monster to return
        StartingPosition = this.transform.position;
        isPatrolling = true;
        isIlluminated = false;
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
            if ((this.transform.position.x <= PlayerPosition.x + 5 && this.transform.position.y <= PlayerPosition.y + 5) &&
                (this.transform.position.x >= PlayerPosition.x - 5 && this.transform.position.y >= PlayerPosition.y - 5))
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
