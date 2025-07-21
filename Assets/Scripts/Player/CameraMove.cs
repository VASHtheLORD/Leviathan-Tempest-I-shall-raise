using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    public Transform orientation;

    public float xRotation;
    public float yRotation;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {
        //Making cursorr invisible and locking it at the senter
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotatin around the x axis(up and down)
        xRotation -= mouseY;

        //Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotation around the y axis (left and right)
        yRotation += mouseX;

        //Apply rotation to our transform
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
