using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float moveSpeed;
    public float rotateSpeed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(0, 0, vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);
    }
}
