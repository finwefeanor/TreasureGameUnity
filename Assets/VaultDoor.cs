using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    public bool isOpen;
    public float openAngle;
    public float closeAngle;
    public float smooth;

    public void ChangeDoorState()
    {
        isOpen = !isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }

    }
}
