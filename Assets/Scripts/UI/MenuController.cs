using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/* MENU CONTROLLER SCRIPT */

public class MenuController : MonoBehaviour {

    public int maxLevel;        // Level (max) available in-game
    public string levelNo;      // Level number

    public void Start()
    {
        maxLevel = 2;   // Setting the max level
    }

    // FUNCTION TO START A FRESH GAME
    public void PlayGame() {

        // LOADING SCENE LEVEL 1
        SceneManager.LoadScene(1);
    }

    // FUNCTION TO LOAD THE NEXT AVAILABLE SCENE
    public void LoadNextLevel(int level) {

        // INCREMENTS LEVEL
        level++;

        // LOADING NEXT SCENE LEVEL
        SceneManager.LoadScene(level);
    }

    // FUNCTION TO MANUALLY SELECT LEVEL FROM MAIN MENU
    /* This function was implemented to allow quick navigation for ease of testing */
    public void SelectLevel() {

        // GETTING THE LEVEL NUMBER
        levelNo = EventSystem.current.currentSelectedGameObject.name;
        var x = int.Parse(levelNo);

        // LOADING THE CORRESPONDING SCENE TO THE BUTTON
        SceneManager.LoadScene(x);
    }

    // FUNCTION TO EXIT GAME
    public void ExitGame() {

        // EXITS APPLICATION
        Application.Quit();
    }

    // FUNCTION TO LOAD THE MAIN MENU
    public void LoadMenu() {

        // LOADING THE MAIN MENU SCENE
        SceneManager.LoadScene(0);
    }
}
