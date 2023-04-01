using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultWheel : MonoBehaviour
{
    public float rotationSpeed;
    public Combination combinationScript;
    private int[] guess;
    private int index;

    void Start()
    {
        guess = new int[3];
        index = 0;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int number = Mathf.RoundToInt(transform.localEulerAngles.y / 10);
            guess[index] = number;
            index++;
            if (index >= 3)
            {
                CheckGuess();
            }
        }
    }

    void CheckGuess()
    {
        if (guess[0] == combinationScript.combination[0] && guess[1] == combinationScript.combination[1] && guess[2] == combinationScript.combination[2])
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("Incorrect combination.");
        }
        index = 0;
    }


}
