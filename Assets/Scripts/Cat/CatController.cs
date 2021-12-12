using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CAT CONTROLLER SCRIPT */
/* Description : Cat object acting as a moving platform between waypoints */

public class CatController : MonoBehaviour
{
    [HideInInspector] public GameObject player;     // Player object
    
    public GameObject[] catWaypoints;               // List of waypoints                
    int catWaypointIndex;                           // Waypoint index
    public Vector3 nextWaypointPosition;            // Next available index

    public float xAngle, yAngle, zAngle;            // Cat (angle) rotation
    public float catSpeed;                          // Cat speed
    public Vector3 catPosition;                     // Cat position


    public void Start() {

        player = GameObject.FindGameObjectWithTag("Player");    // Setting the player object

        catWaypointIndex = 0;                                   // Instantiating the waypoint index
        yAngle = 180;                                           // Setting the y angle rotation
        catSpeed = 1.5f;                                        // Setting the cat speed
    }

    public void Update() {

        // GET THE POSITION OF THE CAT OBJECT
        catPosition = transform.position;

        // SET THE NEXT WAYPOINT POSITION TO GO TO
        nextWaypointPosition = catWaypoints[catWaypointIndex].transform.position;

        // CHECK IF CAT OBJECT HAS REACHED THE WAYPOINT
        if (catPosition == nextWaypointPosition) {

            // INCREMENT THE WAYPOINT INDEX
            catWaypointIndex++;

            // ROTATE THE CAT OBJECT TO FACE NEW WAYPOINT
            transform.Rotate(xAngle, yAngle, zAngle);

            // CHECK IF CAT OBJECT HAS REACHED LAST WAYPOINT
            if (catWaypointIndex >= catWaypoints.Length) {

                // RESET THE WAYPOINT INDEX
                catWaypointIndex = 0;
            }
        }

        // SET THE CAT OBJECT TO MOVE TOWARDS TO THE NEXT WAYPOINT
        transform.position = Vector3.MoveTowards(transform.position, nextWaypointPosition, Time.deltaTime * catSpeed);
    }
}
