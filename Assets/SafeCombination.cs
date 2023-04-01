using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCombination : MonoBehaviour
{
    public GameObject safe_handle;
    public int safe_being_used = 0;
    public float safe_handle_rotation = 0;
    public float rotation_speed = 120;
    public int rotation_direction;
    public float current_rotation_amount = 0;


    public int current_unlock_stage = 0;
    public int current_rotation_no = 0;
    public int[] correct_combination;
    public List<int> correct_combination_separated = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        current_unlock_stage = 0;
        CreateNewSafeCombination();



    }

    // Update is called once per frame
    void Update()
    {
        //SAFE HANDLE ROTATION
        if (safe_being_used == 1)
        {
            current_rotation_amount = current_rotation_amount + rotation_speed * Time.deltaTime;
            safe_handle_rotation = safe_handle_rotation + rotation_direction * rotation_speed * Time.deltaTime;

            if (current_rotation_amount >= 60)
            {
                safe_being_used = 0;
                current_rotation_amount = 0;
                rotation_direction = 0;
            }

            //Handle'ın döndürülmesi
            safe_handle.transform.localEulerAngles = new Vector3(safe_handle.transform.localEulerAngles.x, safe_handle_rotation, safe_handle.transform.localEulerAngles.z);
        }





        if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (safe_being_used == 0)
            {
                TurnCounterClockwise();
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (safe_being_used == 0)
            {
                TurnClockwise();
            }
        }

    }

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

        correct_combination = new int[] { first_number, second_number, third_number };
        correct_combination_separated.Clear();
        for (int i = 0; i < correct_combination.Length; i = i + 1)
        {
            int first_number_c = correct_combination[i];

            if (first_number_c < 0)
            {
                for (int j = 0; j > first_number_c; j = j - 1)
                {
                    correct_combination_separated.Add(-1);
                }
            }
            else if (first_number_c > 0)
            {
                for (int j = 0; j < first_number_c; j = j + 1)
                {
                    correct_combination_separated.Add(1);
                }
            }
        }


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
        current_rotation_no = -1;

        safe_being_used = 1;
        current_rotation_amount = 0;
        rotation_direction = current_rotation_no;


        if (current_rotation_no == correct_combination_separated[current_unlock_stage])
        {
            current_unlock_stage = current_unlock_stage + 1;
            if (current_unlock_stage >= correct_combination_separated.Count)
            {
                current_unlock_stage = 0;
                ResetSafe();
                Debug.Log("SAFE OPENED; RESET SAFE");
            }

        }
        else if (current_rotation_no != correct_combination_separated[current_unlock_stage])
        {
            current_unlock_stage = 0;
            ResetSafe();
            Debug.Log("WRONG MOVE; RESET");
        }
    }


    public void TurnClockwise()
    {
        current_rotation_no = 1;

        safe_being_used = 1;
        current_rotation_amount = 0;
        rotation_direction = current_rotation_no;


        if (current_rotation_no == correct_combination_separated[current_unlock_stage])
        {
            current_unlock_stage = current_unlock_stage + 1;
            if (current_unlock_stage >= correct_combination_separated.Count)
            {
                current_unlock_stage = 0;
                ResetSafe();
                Debug.Log("SAFE OPENED; RESET SAFE");
            }

        }
        else if (current_rotation_no != correct_combination_separated[current_unlock_stage])
        {
            current_unlock_stage = 0;
            ResetSafe();
            Debug.Log("WRONG MOVE; RESET");
        }

    }

    public void ResetSafe()
    {
        CreateNewSafeCombination();

        safe_handle_rotation = 0;
        safe_being_used = 0;
        current_rotation_amount = 0;
        rotation_direction = 0;
        safe_handle.transform.localEulerAngles = new Vector3(safe_handle.transform.localEulerAngles.x, 0, safe_handle.transform.localEulerAngles.z);

    }

}
