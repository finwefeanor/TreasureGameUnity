using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotateSpeed = 5.0f;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private void Update()
    {
        // Get mouse input
        //yaw += Input.GetAxis("Mouse X") * rotateSpeed;
        //pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
        //pitch = Mathf.Clamp(pitch, -40, 60); // limit up/down angle to prevent flipping

        //// Rotate camera based on mouse input
        //transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Position camera based on offset
        transform.position = target.position - transform.forward * offset.magnitude;
    }
}

