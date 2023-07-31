using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : MonoBehaviour
{
    public Text gameOverText; // Assign the Text object you created in the Unity Editor

    private void Start()
    {
        gameOverText.enabled = false;
    }

    public void GameOver()
    {
        gameOverText.enabled = true; // Show the "GAME OVER" text
        StartCoroutine(EndGameDelay()); // End the game after a delay
    }

    private IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(3); // Wait for 3 seconds
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the Unity Editor
#else
            Application.Quit(); // Quit the game if it is a standalone build
#endif
    }
}

