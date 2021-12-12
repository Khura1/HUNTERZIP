using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* HUD CONTROLLER SCRIPT */
/* Description : Refers to the Heads-up display of the player */

public class HUDController : MonoBehaviour
{
    [HideInInspector] public float currentScore;        // Total game score points awarded (progress)
    [HideInInspector] public float totalScore;          // Desired game score points for the game to finish
    [HideInInspector] public float remainingPoints;     // Remaining game score points for the game to finish
    [HideInInspector] public float currentLevel;        // Current scene level

    public Text playerHPText, playerMaxHPText;          // HUD HP text objects
    public Text playerScoreText, playerMaxScoreText;    // HUD Score text objects
    [HideInInspector] public Character character;       // Reference to Character C# Script
    [HideInInspector] public MenuController menu;       // Reference to MenuController C# Script

    public void Start() {

        // REFERENCES
        character = GetComponent<Character>();
        menu = GetComponent<MenuController>();

        currentScore = 0;                                                       // Instantiating the current score
        totalScore = GameObject.FindGameObjectsWithTag("PickupItem").Length;    // Setting the total score (no of pickup items)

        Destroy(GameObject.Find("Objective"), 4);                               // Destroy HUD object (Scene Objective)
    }
    
    public void Update() {

        SetHP();        // Updating player HP for HUD
        SetScore();     // Updating player Score for HUD
    }

    // FUNCTION UPDATING THE HP PROPERTIES
    public void SetHP() {

        playerHPText.text = character.playerHP.ToString();          // Set PlayerHP to HUD
        playerMaxHPText.text = character.playerMaxHP.ToString();    // Set PlayerMaxHP to HUD
    }

    // FUNCTION UPDATING THE SCORE PROPERTIES
    public void SetScore() {

        remainingPoints = GameObject.FindGameObjectsWithTag("PickupItem").Length;   // Setting the remaining score points
        currentScore = totalScore - remainingPoints;                                // Calculating the current score points

        playerScoreText.text = currentScore.ToString();       // Set PlayerScore to HUD
        playerMaxScoreText.text = totalScore.ToString();      // Set PlayerMaxScore to HUD

        // CHECK IF DESIRED TOTAL SCORE HAS BEEN REACHED
        if (totalScore == currentScore) {

            // GET THE CURRENT SCENE LEVEL
            currentLevel = SceneManager.GetActiveScene().buildIndex;

            // CHECK IF CURRENT LEVEL IS NOT MAX LEVEL
            if (currentLevel < menu.maxLevel) {    
                
                // LOADING THE NEXT SCENE LEVEL
                menu.LoadNextLevel((int) currentLevel);
            }

            // CHECK IF CURRENT LEVEL IS MAX LEVEL
            else if (currentLevel == menu.maxLevel) {

                // LOADING THE MAIN MENU
                menu.LoadMenu();
            }
        }
    }
}
