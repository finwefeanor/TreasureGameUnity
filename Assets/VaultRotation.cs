using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultRotation : MonoBehaviour
{
    private int x;
    private int y;
    private int z;
    public int a;
    public int b;
    public int c;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(1, 9);
        y = Random.Range(1, 9);
        z = Random.Range(1, 9);
        Debug.Log("VAULT CODE: " + x + " " +  y + " " + z);     
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (a == x && b == y && c == z)
            {
                transform.Rotate(90, 75, 0.0f);
                Debug.Log("You have won the treasure!");
            }
            else
            {
                print("Wrong combination");
            }
            
        }
    }
}
