using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/* PICKUP ITEM SCRIPT */

public class PickupItem : MonoBehaviour {

    [HideInInspector] public GameObject player;             // Player object
    [HideInInspector] public Rigidbody pickupItem;          // Pickup item object

    public Transform pickupPosition;                        // Pickup item position on player
    [HideInInspector] public bool isKeysEnabled = true;            // Pickup item keys enabling
    [HideInInspector] public bool pickupEnabled;            // Pickup item player enabling

    private float distanceToPlayer;                         // Distance between Player & Pickup item
    public float distanceToPickup;                          // Distance to enable item pickup

    void Start() {

        // REFERENCES
        player = GameObject.FindGameObjectWithTag("Player");
        pickupItem = GetComponent<Rigidbody>();

        //isKeysEnabled = true;   // Instantiating they keys state
        pickupEnabled = false;  // Instantiating the ability to pickup

        distanceToPickup = 1f;  // Setting distance to pickup items

        // ENABLE GRAVITY OF PICKUP ITEM
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void Update() {

        PickupItemEnable();     // Enable player to pickup item upon range
    }

    private void PickupItemEnable() {

        // CALCULATE DISTANCE BETWEEN PICKUP ITEM AND PLAYER
        distanceToPlayer = Vector3.Distance(pickupItem.transform.position, player.transform.position);

        // CHECK IF WITHIN DISTANCE AND ENABLE PICKUP 
        if (distanceToPlayer <= distanceToPickup) {

            pickupEnabled = true;
        }
        else {

            pickupEnabled = false;
        }
    }

    // ON M1 (DOWN) ACTION PLAYER WILL CARRY PICKUP ITEM
    public void OnMouseDown() {

        if ( pickupEnabled == true && pickupPosition != null ) {

            isKeysEnabled = false;                                  // Disable keys
            GetComponent<Rigidbody>().Sleep();                      // Force Rigidbody to sleep

            GetComponent<Rigidbody>().useGravity = false;           // Disable gravity
            GetComponent<Rigidbody>().detectCollisions = false;     // Disable collision detection

            // TRANSFORM POSITION TO PLAYER PICKUP POSITION
            this.transform.position = pickupPosition.position;
            this.transform.parent = GameObject.Find("PlayerIndicator").transform;
        }
    }

    // ON M1 (DOWN) ACTION PLAYER WILL DROP PICKUP ITEM
    public void OnMouseUp() {

        isKeysEnabled = true;                                       // Enable keys
        GetComponent<Rigidbody>().WakeUp();                         // Force Rigidbody to awake

        this.transform.parent = null;                               
        GetComponent<Rigidbody>().useGravity = true;                // Enable gravity
        GetComponent<Rigidbody>().detectCollisions = true;          // Enable collision detection
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Projectile") {

            // CHECK TO DESTROY PICKUP ITEM IF LEVEL 2 IS ACTIVE SCENE
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StorageScene"))
            {
                Destroy(gameObject);
            }
        }
    }
}
