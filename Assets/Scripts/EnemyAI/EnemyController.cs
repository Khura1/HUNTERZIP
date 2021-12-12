using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* ENEMY CONTROLLER SCRIPT */

public class EnemyController : MonoBehaviour {

    [HideInInspector] public GameObject player;               // Player object
    [HideInInspector] public NavMeshAgent enemyAI;            // Enemy object

    public float enemyHP;                   // EnemyAI Health (HitPoints)
    public float enemyMaxHP = 2;            // EnemyAI MAX Health (HitPoints)
    public float enemySpeed = 1.25f;        // EnemyAI Speed

    public float enemyFollowRange = 3f;     // Range trigger to follow Player
    private float distanceToPlayer;         // Distance between Player & EnemyAI
    
    private float generalTimer;             // General timer
    public float enemyWanderTimer = 10f;    // Wandering timer by EnemyAI
    public float enemyWanderRange = 10f;    // Wandering radius by EnemyAI

    public void Start() {

        // REFERENCES
        enemyAI = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyHP = enemyMaxHP;               // Setting up the EnemyAI HP

        generalTimer = enemyWanderTimer;    // Sync general timer w/ EnemyAI wander timer
    }

    public void Update() {

        EnemyMovementWander();              // WANDER : EnemyAI wander at random positions
        EnemyMovementProximity();           // PROXIMITY : EnemyAI follows player at custom proximity
    }

    private void EnemyMovementWander() {

        // ADDING EACH FRAME SECOND(S)
        generalTimer += Time.deltaTime;

        if (generalTimer >= enemyWanderTimer) {

            // SET NEW RANDOM DESTINATION FOR ENEMY AI TO GO TO
            Vector3 destination = RandomNavSphere(transform.position, enemyWanderRange, -1);
            enemyAI.SetDestination(destination);

            // RESET THE GENERAL TIMER
            generalTimer = 0;
        }
    }

    private void EnemyMovementProximity() { 
        
        // CALCULATE DISTANCE OF PLAYER FROM ENEMY
        distanceToPlayer = Vector3.Distance(enemyAI.transform.position, player.transform.position);

        if (distanceToPlayer <= enemyFollowRange) {

            // SETTING THE ENEMY AI TO GO TO THE PLAYER
            enemyAI.SetDestination(player.transform.position);
        }
    }

    // CREATES A RANDOM NAVIGATION SPHERE
    public static Vector3 RandomNavSphere(Vector3 originPosition, float radius, int layermask) {

        Vector3 randDirection = Random.insideUnitSphere * radius;
        randDirection += originPosition;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, radius, layermask);

        return navHit.position;
    }

    void OnCollisionEnter(Collision collision) {

        // HANDLE COLLISION WITH PROJECTILE
        if ( collision.collider.tag == "Projectile") {

            // ENEMY AI RECEIVES DAMAGE
            Debug.Log("Enemy damaged : -1 HP");
            enemyHP = enemyHP - 1;

            // ENEMY AI DIES IF NO HEALTH
            if ( enemyHP <= 0 && gameObject != null ) {
                Destroy(gameObject);
            }
        }
    }
}
