using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCombination2 : MonoBehaviour
{
    public GameObject safeHandle;
    public GameObject safeDoor;
    public GameObject treasure;
    public ParticleSystem glitterEffect;
    public GameObject unlockSound;
    public GameObject vaultOpenSound;

    public bool safeBeingUsed = false;
    public float safeHandleRotation = 0;
    public float rotationSpeed = 120;
    public int rotationDirection;
    public float currentRotationAmount = 0;

    public int currentUnlockStage = 0;
    public int currentRotationNo = 0;
    public int[] correctCombination;
    public List<int> correctCombinationSeparated = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        safeDoor.GetComponent<Animator>().enabled = false;
        
        currentUnlockStage = 0;
        CreateNewSafeCombination();
        treasure.SetActive(false);
        glitterEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Safe handle rotation
        if (safeBeingUsed)
        {
            currentRotationAmount = currentRotationAmount + rotationSpeed * Time.deltaTime;
            safeHandleRotation = safeHandleRotation + rotationDirection * rotationSpeed * Time.deltaTime;

            if (currentRotationAmount >= 60)
            {
                safeBeingUsed = false;
                currentRotationAmount = 0;
                rotationDirection = 0;
            }

            safeHandle.transform.localEulerAngles = new Vector3(safeHandle.transform.localEulerAngles.x,
                safeHandleRotation, safeHandle.transform.localEulerAngles.z);
            
        }

        // Handle user input
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!safeBeingUsed)
            {
                TurnCounterClockwise();
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!safeBeingUsed)
            {
                TurnClockwise();
            }
        }
    }

    // Add the CreateNewSafeCombination method from the original code
    public void CreateNewSafeCombination()
    {
        int first_direction = Random.Range(-1, 1);
        if (first_direction < 0)
        {
            first_direction = -1;
        }
        else if (first_direction >= 0)
        {
            first_direction = 1;
        }

        int first_number = first_direction * Random.Range(1, 10);
        int second_number = first_direction * (-1) * Random.Range(1, 10);
        int third_number = first_direction * Random.Range(1, 10);

        correctCombination = new int[] { first_number, second_number, third_number };
        correctCombinationSeparated.Clear();
        for (int i = 0; i < correctCombination.Length; i = i + 1)
        {
            int first_number_c = correctCombination[i];

            if (first_number_c < 0)
            {
                for (int j = 0; j > first_number_c; j = j - 1)
                {
                    correctCombinationSeparated.Add(-1);
                }
            }
            else if (first_number_c > 0)
            {
                for (int j = 0; j < first_number_c; j = j + 1)
                {
                    correctCombinationSeparated.Add(1);
                }
            }
        }

        Debug.Log("The combination is " + correctCombination[0] + correctCombination[1] + correctCombination[2]);


        //correct_combination = new int[] { -3, 2, -4 };
        //correct_combination_separated.Clear();
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(1);
        //correct_combination_separated.Add(1);
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(-1);
        //correct_combination_separated.Add(-1);

        //correct_combination_separated = new int[] { -1, -1, -1, 1, 1, -1, -1, -1, -1 };

    }

    public void TurnCounterClockwise()
    {
        currentRotationNo = -1;
        safeBeingUsed = true;
        currentRotationAmount = 0;
        rotationDirection = currentRotationNo;
        safeDoor.GetComponent<AudioSource>().enabled = true;
        safeDoor.GetComponent<AudioSource>().Play();

        CheckMoveAndUpdateStage();
    }

    public void TurnClockwise()
    {
        currentRotationNo = 1;
        safeBeingUsed = true;
        currentRotationAmount = 0;
        rotationDirection = currentRotationNo;
        safeDoor.GetComponent<AudioSource>().enabled = true;
        safeDoor.GetComponent<AudioSource>().Play();       



        CheckMoveAndUpdateStage();
    }

    private void CheckMoveAndUpdateStage()
    {
        if (currentRotationNo == correctCombinationSeparated[currentUnlockStage])
        {
            currentUnlockStage++;
            if (currentUnlockStage >= correctCombinationSeparated.Count)
            {
                safeDoor.GetComponent<Animator>().enabled = true;
                unlockSound.GetComponent<AudioSource>().enabled = true;
                unlockSound.GetComponent<AudioSource>().Play();
                
                OpenSafe();
            }
        }
        else
        {
            StartCoroutine(WrongMoveReset());
        }
    }

    private void OpenSafe()
    {
        Debug.Log("Safe Opened!");
        if (safeDoor.GetComponent<Animator>().enabled == true)
        {
            safeDoor.GetComponent<AudioSource>().enabled = false;
            vaultOpenSound.GetComponent<AudioSource>().enabled = true;
            vaultOpenSound.GetComponent<AudioSource>().Play();
            safeDoor.GetComponent<Animator>().Play("Base Layer.newSafeAnim", 0);
        }
        //safeDoor.GetComponent<Animator>().enabled = true;
        
        treasure.SetActive(true);
        glitterEffect.Play();
        currentUnlockStage = 0;
        CreateNewSafeCombination();
    }

    private IEnumerator WrongMoveReset()
    {
        Debug.Log("Wrong Move; Reset");
        safeBeingUsed = true;
        rotationSpeed *= 3;
        rotationDirection = 1;
        yield return new WaitForSeconds(1.5f);
        rotationSpeed /= 3;
        safeBeingUsed = false;
        currentUnlockStage = 0;
        CreateNewSafeCombination();
    }
}
