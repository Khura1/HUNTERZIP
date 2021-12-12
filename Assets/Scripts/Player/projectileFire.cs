using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* PROJECTILE FIRE SCRIPT */

public class projectileFire : MonoBehaviour {

    public Rigidbody projectileBullet;          // Projectile object (rigidbody)
    public float projectileSpeed;               // Projectile speed
    public float projectileFireRate;            // Projectile rate of fire
    public float projectileShoot;               // Projectile timer to shoot next shot
    public float projectileLife;                // Projectile max time to exist on field

    public void Start() {

        projectileSpeed = 10f;
        projectileFireRate = 0.75f;
        projectileShoot = 0f;
        projectileLife = 1f;
    }

    private void Update() {

        // LINK W/ PICKUPITEM.CS
        PickupItem _pickupItem = FindObjectOfType<PickupItem>();

        // SHOOT PROJECTILE ACTION ( WHEN COOLDOWN IS REACHED & WHEN NO PICKUP ITEM IS IN POSSESSION )
        if ( Input.GetKey(KeyCode.E) && Time.time > projectileShoot && _pickupItem.isKeysEnabled ) {

                FireProjectile();
        }
    }

    public void FireProjectile() {

        // LINK W/ PICKUPITEM.CS
        PickupItem _pickupItem = FindObjectOfType<PickupItem>();

        // CALCULATE WHEN TO SHOOT
        projectileShoot = Time.time + projectileFireRate;

        // CREATE A NEW PROJECTILE ( AS RIGIDBODY )
        Rigidbody Projectile = Instantiate( projectileBullet,
                                            transform.position,
                                            transform.rotation) as Rigidbody;

        // CALCULATE THE VELOCITY OF THE PROJECTILE
        Projectile.velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));

        // DESTROY THE OBJECT AFTER A CERTAIN TIME
        Destroy(Projectile.gameObject, projectileLife);
        
    }
}
