using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gameOverText; // Assign the Text object you created in the Unity Editor
    public GameObject detectedSound;
    private AudioSource detectedSoundAudio;

    private void Start()
    {
        detectedSoundAudio = detectedSound.GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        Debug.Log("Enabling game over text...");
        //gameOverText.enabled = true; // Show the "GAME OVER" text
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(EndGameDelay()); // End the game after a delay
    }

    public void DetectedSoundPlay()
    {
        detectedSoundAudio.enabled = true;
        detectedSoundAudio.gameObject.SetActive(true);
        detectedSoundAudio.Play();
    }

    private IEnumerator EndGameDelay()
    {
        
        //gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3); // Wait for 3 seconds
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the Unity Editor
#else
            Application.Quit(); // Quit the game if it is a standalone build
#endif
    }
}

