using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    public int[] combination;

    void Start()
    {
        combination = new int[3];
        for (int i = 0; i < combination.Length; i++)
        {
            combination[i] = Random.Range(0, 10);
        }
        Debug.Log("The combination is " + combination[0] + combination[1] + combination[2]);
    }

}
