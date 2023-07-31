using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float accelerationTime = 1f;
    public float decelerationTime = 0.5f;
    private Vector3 targetVelocity;
    private Vector3 currentVelocity;
    Vector3 moveDirection;

    public float rotationSpeed = 5.0f;
    public float pitch;

    private CharacterController controller;
    float smoothTime;

    private Transform cameraTransform;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Get the move direction relative to the camera's rotation
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        // Determine the target velocity based on whether the player is trying to move
        if (moveDirection != Vector3.zero)
        {
            targetVelocity = moveDirection * moveSpeed;
        }
        else
        {
            targetVelocity = Vector3.zero;
        }

        // Gradually change the velocity based on the acceleration or deceleration time
        float smoothTime;
        if (currentVelocity == Vector3.zero)
        {
            smoothTime = accelerationTime;
        }
        else
        {
            smoothTime = decelerationTime;
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref currentVelocity, smoothTime);

        // Apply the movement
        controller.Move(currentVelocity * Time.deltaTime);

        // Rotate the player based on the mouse's X-axis movement
        //float turn = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        //transform.Rotate(0, turn, 0);

        // Checking if the left mouse button is clicked or not
        if (Input.GetMouseButtonDown(0))
        {
            // Locking the cursor at the center of the screen when the left mouse button is clicked
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Checking if the Escape key is pressed or not
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Unlocking the cursor when the Escape key is pressed
            Cursor.lockState = CursorLockMode.None;
        }

        // Rotate the player only when the cursor is locked
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Get the move direction relative to the camera's rotation
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;

            // Determine the target velocity based on whether the player is trying to move
            if (moveDirection != Vector3.zero)
            {
                targetVelocity = moveDirection * moveSpeed;
            }
            else
            {
                targetVelocity = Vector3.zero;
            }

            // Gradually change the velocity based on the acceleration or deceleration time
            
            if (currentVelocity == Vector3.zero)
            {
                smoothTime = accelerationTime;
            }
            else
            {
                smoothTime = decelerationTime;
            }
            currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref currentVelocity, smoothTime);

            // Apply the movement
            controller.Move(currentVelocity * Time.deltaTime);

            // Rotate the player based on the mouse's X-axis movement
            float turn = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
            transform.Rotate(0, turn, 0);
        }

    }
}
