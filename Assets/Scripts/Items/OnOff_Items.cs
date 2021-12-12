using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ON/OFF ITEMS SCRIPT */
/* Description : Current implementation of this script is to turn on/off items (e.g. lamp light) */

public class OnOff_Items : MonoBehaviour
{
    public GameObject lampLight;    // Lamp light object

    public void Start() {

        // GET LAMP LIGHT OBJECT
        lampLight = GameObject.FindWithTag("lampLight");

        // DEACTIVATE LAMP LIGHT OBJECT
        lampLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider) {

        // ACTIVATE LAMP LIGHT OBJECT
        lampLight.SetActive(true);
    }

    private void OnTriggerExit(Collider collider) {

        // DEACTIVATE LAMP LIGHT OBJECT
        lampLight.SetActive(false);
    }
}
