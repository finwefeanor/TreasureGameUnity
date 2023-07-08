using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeCombination2 : MonoBehaviour
{
    public GameObject safeHandle;
    public GameObject safeDoor;
    public GameObject treasure;
    public ParticleSystem glitterEffect;
    public GameObject unlockSound;
    public GameObject vaultOpenSound;

    public bool isSafeBeingUsed = false;
    public float safeHandleRotation = 0;
    public float rotationSpeed = 120;
    public int handleRotationDirection;
    public float currentRotationAmount = 0;

    public int currentUnlockStage = 0;
    public int currentRotationDirection = 0;
    public int[] correctCombination;
    public List<int> correctCombinationSeparated = new List<int>();
    public Text timerText;
    private float startTime;

    private AudioSource safeDoorAudio;
    private AudioSource unlockSoundAudio;
    private AudioSource vaultOpenSoundAudio;

    // Start is called before the first frame update
    void Start()
    {
        safeDoor.GetComponent<Animator>().enabled = false;
        
        currentUnlockStage = 0;
        CreateNewSafeCombination();
        treasure.SetActive(false);
        glitterEffect.Stop();
        safeDoorAudio = safeDoor.GetComponent<AudioSource>();
        unlockSoundAudio = unlockSound.GetComponent<AudioSource>();
        vaultOpenSoundAudio = vaultOpenSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;

        timerText.text = "Time: " + elapsedTime.ToString("F1"); //F1 means to format the float with 1 decimal place
        // Safe handle rotation
        if (isSafeBeingUsed)
        {
            currentRotationAmount = currentRotationAmount + rotationSpeed * Time.deltaTime;
            safeHandleRotation = safeHandleRotation + handleRotationDirection * rotationSpeed * Time.deltaTime;

            if (currentRotationAmount >= 60)
            {
                isSafeBeingUsed = false;
                currentRotationAmount = 0;
                handleRotationDirection = 0;
            }

            safeHandle.transform.localEulerAngles = new Vector3(safeHandle.transform.localEulerAngles.x,
                safeHandleRotation, safeHandle.transform.localEulerAngles.z);
            
        }

        // Handle user input
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isSafeBeingUsed)
            {
                TurnCounterClockwise();
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!isSafeBeingUsed)
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
        currentRotationDirection = -1;
        isSafeBeingUsed = true;
        currentRotationAmount = 0;
        handleRotationDirection = currentRotationDirection;
        safeDoorAudio.enabled = true;
        safeDoorAudio.Play();

        CheckMoveAndUpdateStage();
    }

    public void TurnClockwise()
    {
        currentRotationDirection = 1;
        isSafeBeingUsed = true;
        currentRotationAmount = 0;
        handleRotationDirection = currentRotationDirection;
        safeDoorAudio.enabled = true;
        safeDoorAudio.Play();
        CheckMoveAndUpdateStage();
    }

    private void CheckMoveAndUpdateStage()
    {
        if (currentRotationDirection == correctCombinationSeparated[currentUnlockStage])
        {
            currentUnlockStage++;
            if (currentUnlockStage >= correctCombinationSeparated.Count)
            {
                safeDoorAudio.enabled = true;
                unlockSoundAudio.enabled = true;
                unlockSoundAudio.Play();
                
                OpenSafe();
            }
        }
        else
        {
            StartCoroutine(WrongMoveReset());
        }
    }

    private IEnumerator DelayBeforeReset()
    {
        yield return new WaitForSeconds(1); // delay for 2 seconds before resetting
        ResetSafe();
    }
    private void OpenSafe()
    {
        safeDoor.GetComponent<Animator>().enabled = true;
        Debug.Log("Safe Opened!");

        safeDoorAudio.enabled = false;
        vaultOpenSoundAudio.enabled = true;
        vaultOpenSoundAudio.Play();
        safeDoor.GetComponent<Animator>().Play("Base Layer.newSafeAnim", 0);

        // wait for 5 seconds before closing the safe
        Invoke("CloseSafe", 5f);

        treasure.SetActive(true);
        glitterEffect.Play();
        currentUnlockStage = 0;
        Debug.Log("SAFE OPENED; RESET SAFE");
        // Don't reset the safe immediately after it opens
    }

    private void CloseSafe()
    {
        // Plays the closing animation
        safeDoor.GetComponent<Animator>().Play("closeDoorAnim");
        glitterEffect.Stop();

        // Reset the safe after the animation ends
        // Adjust this delay to match the length of your closing animation
        Invoke("ResetSafe", 1f);
        
    }

    private IEnumerator WrongMoveReset()
    {
        Debug.Log("Wrong Move; Reset");
        isSafeBeingUsed = true;
        rotationSpeed *= 3;
        handleRotationDirection = 1;
        yield return new WaitForSeconds(1.5f);
        rotationSpeed /= 3;
        isSafeBeingUsed = false;
        currentUnlockStage = 0;

        ResetSafe();      
        
    }
    public void ResetSafe()
    {
        CreateNewSafeCombination();

        startTime = Time.time;
        safeHandleRotation = 0;
        isSafeBeingUsed = false;
        currentRotationAmount = 0;
        handleRotationDirection = 0;
        safeHandle.transform.localEulerAngles = new Vector3
            (safeHandle.transform.localEulerAngles.x, 0, safeHandle.transform.localEulerAngles.z);
    }
}
