using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Serialization;
using static PauseScreen;
using static GameStartPrompt;

public class PlayerMovement : MonoBehaviour
{
    Camera viewCamera;
    public float moveSpeed = 2f;
    
    public Rigidbody2D rigidbody;
    private Animator playerAnimation;

    private Vector2 _movement;
    private Vector2 StartingPosition;
    public int playerCourage = 3;
    public bool isCaught;

    private AudioSource playerMovementSound;
    private PlayableDirector fadeToBlack;

    public TMP_Text overheadSpeech;
    private int _currentBears;
    private string startText;
    public string endText = "Found all the bears. I can go to sleep now! Time to go to bed";
    
    public PlayerDirection currentDirection;
    
    //enum for player direction
    public enum PlayerDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    [FormerlySerializedAs("_fieldOfView")] [SerializeField] private LanturnLightFieldOfView lanturnLightFieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        getRemainingBears();
        startText = "I can't sleep without my " + _currentBears.ToString() + " bears! I have to find them!";
        printSpeech(startText);

        isCaught = false;
        rigidbody = GetComponent<Rigidbody2D>();
        StartingPosition = this.transform.position;

        playerAnimation = GetComponent<Animator>();
        playerMovementSound = GetComponent<AudioSource>();

}


    // Update is called once per frame
    void Update()
    {
        if (playerCourage > 0)
        {
            
            //user input
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            currentDirection = _movement.x switch
            {
                //set current direction enum
                > 0 => PlayerDirection.Right,
                < 0 => PlayerDirection.Left,
                _ => _movement.y switch
                {
                    > 0 => PlayerDirection.Up,
                    < 0 => PlayerDirection.Down,
                    _ => currentDirection
                }
            };

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (!playerMovementSound.isPlaying)
                {
                    playerMovementSound.Play();
                }
            }
            else
            {
                playerMovementSound.Stop();
            }
        }
    }

   
    private void FixedUpdate()
    {
        if (!isCaught)
        {
            if (lanturnLightFieldOfView == null) return;
            // Movement
            rigidbody.MovePosition(rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
            lanturnLightFieldOfView.SetOrigin(rigidbody.position);
            //Animation will go here
            // playerAnimation.SetFloat("PlayerMoveX", _movement.x);
            // playerAnimation.SetFloat("PlayerMoveY", _movement.y);

            //use enum direction to set player direction
            playerAnimation.SetFloat("PlayerMoveX", currentDirection == PlayerDirection.Right ? 1 : currentDirection == PlayerDirection.Left ? -1 : 0);
            playerAnimation.SetFloat("PlayerMoveY", currentDirection == PlayerDirection.Up ? 1 : currentDirection == PlayerDirection.Down ? -1 : 0);
        }
    }

    private void OnCollisionEnter2D ( Collision2D objectColliding )
    {
        if (!isCaught)
        {
            if (objectColliding.gameObject.CompareTag("Enemy"))
            {
                caught();
            }
        }
    }

    /**
     *  GOVERNS OVERHEAD TEXT 
     **/
    public void printSpeech( string _speech )
    {
        overheadSpeech.text = _speech;
        StartCoroutine(clearSpeech(4f));
    }

    private IEnumerator clearSpeech( float _time )
    {
        yield return new WaitForSeconds(_time);
        overheadSpeech.text = "";
    }

    /**
    *  GOVERNS IF CAUGHT BY MONSTERS    
    **/
    public void caught()
    {
        isCaught = true;
        fadeToBlack = GameObject.Find("GameManager").GetComponent<GameManager>().fadeOut;
        fadeToBlack.Play();
        StartCoroutine(noLongerCaught(2f));
    }

    private IEnumerator noLongerCaught( float _time )
    {
        yield return new WaitForSeconds(_time);
        this.transform.position = StartingPosition;
        playerCourage = playerCourage - 1;
        lanturnLightFieldOfView.ResetLightAngleAndLength();
        isCaught = false;
    }

    public void getRemainingBears()
    {
        _currentBears = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }
}
