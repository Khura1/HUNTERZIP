using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* UNUSED SCRIPT */

public class Platform : MonoBehaviour
{
    public float speed = 2f;

    private void OnTriggerEnter(Collider collider)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Box")
        {
            speed = 0f;
        }
    }
}

