using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

/* SCORE POINTS SCRIPT */
/* Description : Dedicated object that destroys item upon collection to score */

public class ScorePoints : MonoBehaviour { 

    public void Start() {

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;     // Free position (Rigidbody)
        GetComponent<Rigidbody>().freezeRotation = true;                            // Freeze rotation (Rigidbody)
    }

    public void OnCollisionEnter(Collision collision) {

        // HANDLE COLLISION WITH PICKUP ITEM
        if (collision.collider.tag == "PickupItem") {

            Destroy(collision.gameObject);  // Pickup item is destoryed
        }
    }
}