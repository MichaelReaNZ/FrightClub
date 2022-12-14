using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*****
     * 
     *  VARIABLES
     * 
     *****/

    //Illumination and Patrol States
    public bool isIlluminated;
    protected bool isPatrolling;

    //Positions of player and monster
    private Vector2 PlayerPosition;
    protected Vector2 StartingPosition;

    //Patrol Values, StartingPosition is considered the first mark
    public Vector2 secondMark;
    public Vector2 thirdMark;
    public Vector2 fourthMark;
    private Vector2[] patrolMarks;
    protected int currentMark;

    //Sounds
    private AudioSource monsterSound;

    //Speeds
    private float Speed;
    public float HiddenSpeed;
    public float VisibleSpeed;

    //Detection Radius
    public int PlayerDetectionRadius;
    public int PlayerTrackingRadius;

    //Return to start
    private bool returning;

    //chnage sprite
    public Sprite newSprite, originalSprite;

    // change enemy tag
    public string newtag;
    public string startingtag;

    public Vector3 Originalscale;
    public Vector3 newscale;
    public Vector3 Hitbox;
    public Vector2 HitBoxOffset;



    /*****
     * 
     *  FUNCTIONS
     * 
     *****/

    // Start is called before the first frame update
    void Start()
    {
        //Assign starting point for the monster to return
        StartingPosition = this.transform.position;
        isPatrolling = true;
        isIlluminated = false;
        Speed = HiddenSpeed;
        returning = false;
        monsterSound = GetComponent<AudioSource>();



        //Patrol Values setup
        patrolMarks = new Vector2[4]
        {
            StartingPosition,
            secondMark,
            thirdMark,
            fourthMark
        };
        currentMark = 0;



        //Sets sound
        //monsterSound = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //Search for the player
        PlayerPosition = GameObject.Find("Player").transform.position;

        //Checks for illumination status and confers behaviour based on it
        if (!isIlluminated)
        {
            Speed = HiddenSpeed;
            HiddenBehaviour();
        }
        else
        {
            Speed = VisibleSpeed;
            IlluminatedBehaviour();
        }
    }

    //The AI for a monster if it is hidden
    protected virtual void HiddenBehaviour()
    {
        gameObject.transform.localScale = Originalscale;
        gameObject.tag = startingtag;

        if (DetectPlayer() && !AwayFromStart() && !returning && !PlayerFleeing() )
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
                ReturnToStart();
            }
        }
    }

    //The AI for a monster if it is in the light of the lantern
    protected virtual void IlluminatedBehaviour()
    {
        //DO NOTHING
        gameObject.tag = newtag;
        gameObject.transform.localScale = newscale;
    }

    protected bool PlayerFleeing()
    {
        return GameObject.Find("Player").GetComponent<PlayerMovement>().isCaught;
    }

    //Detect if the player is nearby the monster
    protected bool DetectPlayer()
    {
        if (this.transform.position.x <= PlayerPosition.x + PlayerDetectionRadius && this.transform.position.y <= PlayerPosition.y + PlayerDetectionRadius &&
             this.transform.position.x >= PlayerPosition.x - PlayerDetectionRadius && this.transform.position.y >= PlayerPosition.y - PlayerDetectionRadius)
            return true;
        else
            return false;
    }

    // Detect if Monster is away from its starting position by the length of PlayerTrackingRadius
    protected bool AwayFromStart()
    {
        if (this.transform.position.x <= StartingPosition.x + PlayerTrackingRadius && this.transform.position.y <= StartingPosition.y + PlayerTrackingRadius &&
             this.transform.position.x >= StartingPosition.x - PlayerTrackingRadius && this.transform.position.y >= StartingPosition.y - PlayerTrackingRadius)
            return false;
        else
            return true;
    }

    //Move close to the player to attack them
    protected void Attack()
    {

        this.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Speed);
    }

    //Return to starting position
    protected void ReturnToStart()
    {
        returning = true;
        this.transform.position = Vector2.MoveTowards(transform.position, StartingPosition, Speed);
        if ((Vector2)this.transform.position == StartingPosition)
        {
            isPatrolling = true;
        }
    }

    // Move along set routes, points must be implemented into the editor on creation
    protected virtual int Patrol(int pointToMove)
    {
        this.transform.position = Vector2.MoveTowards(transform.position, patrolMarks[pointToMove], Speed);
        if (this.transform.position.x == patrolMarks[pointToMove].x && this.transform.position.y == patrolMarks[pointToMove].y)
        {
            pointToMove++;
            if (pointToMove == patrolMarks.Length)
            {
                pointToMove = 0;
            }
        }
        return pointToMove;
    }

    protected void OnCollision2D(Collision2D collidingObject)
    {
        if (collidingObject.gameObject.tag == "Wall")
        {
            ReturnToStart();
        }
    }

    // chnage monster sprite
    void Update()
    {
        if (!isIlluminated)
        {
            
            GetComponent<SpriteRenderer>().sprite = originalSprite;
            GetComponent<BoxCollider2D>().size = Hitbox;
            GetComponent<BoxCollider2D>().offset = HitBoxOffset;
        }

        else
        {
            
            GetComponent<SpriteRenderer>().sprite = newSprite;
            GetComponent<BoxCollider2D>().size = Hitbox;
            GetComponent<BoxCollider2D>().offset = HitBoxOffset;
        }

    }
}