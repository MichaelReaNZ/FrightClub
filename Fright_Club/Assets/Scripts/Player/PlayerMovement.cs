using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera viewCamera;
    public float moveSpeed = 5f;
    
    public Rigidbody2D rigidbody;
    private Animator playerAnimation;

    private Vector2 _movement;
    private Vector2 StartingPosition;
    public int PlayerHealth;
    public bool VictoryLocation;

    private AudioSource playerMovementSound;
    
    public PlayerDirection currentDirection;
    
    //enum for player direction
    public enum PlayerDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    [SerializeField] private FieldOfView _fieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 3;
        rigidbody = GetComponent<Rigidbody2D>();
        StartingPosition = this.transform.position;
        VictoryLocation = false;

        playerAnimation = GetComponent<Animator>();
        playerMovementSound = GetComponent<AudioSource>();
}


    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth > 0)
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
        if(_fieldOfView == null) return;
        // Movement
        rigidbody.MovePosition(rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        _fieldOfView.SetOrigin(rigidbody.position);
        playerAnimation.SetFloat("PlayerMoveX", _movement.x);
        playerAnimation.SetFloat("PlayerMoveY", _movement.y);
    }

    private void OnCollisionEnter2D ( Collision2D objectColliding )
    {
        if (objectColliding.gameObject.CompareTag("Enemy"))
        {
            this.transform.position = StartingPosition;
            PlayerHealth = PlayerHealth - 1;
            _fieldOfView.ResetLightAngleAndLength();
        }
        else
        {
            if(objectColliding.gameObject.CompareTag("Finish")  )
            {
                VictoryLocation = true;
            }
        }
    }

    private void onCollisionExit2D ( Collision2D objectColliding )
    {
        if (objectColliding.gameObject.CompareTag("Finish"))
        {
            VictoryLocation = false;
        }
    }

}
