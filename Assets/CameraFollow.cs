using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // the object to follow
    public float smoothing = 5.0f; // the speed at which the camera catches up

    public Vector3 offset;
    float verticalRotation = 0.0f; // Add this at the top of your script
    public float rotationSpeed = 100.0f;

    private void Start()
    {
        // calculate the initial offset
        //offset = transform.position - target.position;
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // New code for vertical rotation
            float verticalMovement = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            verticalRotation -= verticalMovement; // subtract to invert the vertical axis
            verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f); // prevent from overturning

            // Apply the rotations
            transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
