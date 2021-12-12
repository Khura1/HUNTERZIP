using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

/* CHARACTER SCRIPT */

public class Character : MonoBehaviour {

    [HideInInspector] public CharacterController controller;

    public float runSpeed;                  // Player assigned Speed
    public float playerPickupSpeed;         // Player Speed (w/ pickup item)

    public float rotationSpeed;             // Player Rotation Speed
    public float jumpSpeed;                 // Player Jump Speed

    private Vector3 moveDirection;          // Move Direction
    private float moveDirectionY;           // Move Direction (Y Vector)

    public float gravity;                   // Gravity
    public float gravityEarth;              // Gravity (Earth)
    public float gravityJumpPad;            // Gravity (jump pad)

    public bool isPlayerDead;                               // Player Status (alive/dead)

    public float playerDMGTaken;                            // Player DMG taken
    [SerializeField] public float playerHP;                 // Player Health (HitPoints)
    [SerializeField] public float playerMaxHP;              // Player MAX Health (HitPoints)

    [HideInInspector] private HUDController hud;            // Reference to HUDController C# Script 
    [HideInInspector] public MenuController menu;           // Reference to MenuController C# Script

    [HideInInspector] public GameObject[] pickupItems;      // List of pickup items


    public AudioSource newAudio;    // Audio object
    public AudioClip clip;          // Clip object
    public float volume;            // Clip Volumbe

    public RaycastHit hit;     // Returned raycast hit
    public Ray ray;

    public void Start() {

        // REFERENCES
        hud = GetComponent<HUDController>();
        menu = GetComponent<MenuController>();
        newAudio = GetComponent<AudioSource>();

        playerMaxHP = 100;          // Setting up player max hp
        playerHP = playerMaxHP;     // Setting up the player HP

        gravityEarth = 9.8f;        // Setting up gravity
        gravityJumpPad = 0.5f;      // Setting up jumppad gravity
        gravity = gravityEarth;     // Setting up gravity (Earthly gravity)
        
        runSpeed = 1.25f;           // Setting the player speed
        playerPickupSpeed = 0.5f;   // Setting up player pickup speed
        rotationSpeed = 15f;        // Setting up rotation speed
        jumpSpeed = 3f;             // Setting up jump speed

        playerDMGTaken = 1;         // Setting up damage points taken

        volume = 0.1f;              // Setting up clip volume

        // FREEZE POSITION & ROTATION OF PLAYER
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // Freeze Position (Rigidbody)
        GetComponent<Rigidbody>().freezeRotation = true;                        // Freeze Rotation (Rigidbody)
    }

    public void Update() {

        CharacterControl();             // Character movement Control
        RotateCharacterByMouse();       // Mouse movement Control
    }

    public void CharacterControl() {

        // GET CHARACTER CONTROLLER
        controller = GetComponent<CharacterController>();

        // GET INPUT OF COORDINATES & JUMP ACTION
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        // MOVE PLAYER BASED ON INPUT
        moveDirection = new Vector3(hInput, 0, vInput) * runSpeed;

        //JUMP ACTION ( WHEN PLAYER IS GROUNDED & WHEN NO PICKUP ITEM IS IN POSSESSION )
        if ( controller.isGrounded && Input.GetButtonDown("Jump") ) {

            moveDirectionY = jumpSpeed;
        }

        // ENFROCE GRAVITY
        moveDirectionY -= gravity * Time.deltaTime;
        moveDirection.y = moveDirectionY;

        // APPLY PLAYER MOVEMENT        
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void RotateCharacterByMouse() {

        // GET MOUSE POSITION ACCORDING TO MAIN CAMERA
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ROTATE PLAYER ACCORDING TO MOUSE POSITION
        if (Physics.Raycast(ray, out hit)) {

            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    void OnCollisionStay(Collision collision) {

        // HANDLE COLLISION WITH ENEMY
        if (collision.gameObject.tag == "EnemyAI") {

            playerHP -= playerDMGTaken;     // Player loses HP
            hud.SetHP();                    // Updates HUD with current HP

            // LOAD MAIN MENU SCREEN WHEN PLAYER HP HAS REACHED 0
            if (playerHP <= 0 ) {

                playerHP = playerMaxHP;     // Setting up the PlayerHP
                menu.LoadMenu();            // Loads main menu
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            // GET CHARACTER CONTROLLER
            controller = GetComponent<CharacterController>();

            if (collision.gameObject.tag == "JumpPadItem")
            {
                gravity = gravityJumpPad;           // Set gravity (JumpPad)
                newAudio.PlayOneShot(newAudio.clip, volume);

                // RESET GRAVITY AFTER TIMER
                StartCoroutine(ExecuteAfterTime(2));
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time) {

        yield return new WaitForSeconds(time);  // Code execution delay
        gravity = gravityEarth;                 // Set gravity (Earth)
    }
}


