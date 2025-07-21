using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorRotation : MonoBehaviour
{
    public CameraMove cm;

    public Transform orientation;

    private void Start()
    {
        
    }

    void Update()
    {
        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * cm.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cm.mouseSensitivity * Time.deltaTime;

        //Rotatin around the x axis(up and down)
        cm.xRotation -= mouseY;

        //Clamp the rotation
        cm.xRotation = Mathf.Clamp(cm.xRotation, cm.topClamp, cm.bottomClamp);

        //Rotation around the y axis (left and right)
        cm.yRotation += mouseX;

        //Apply rotation to our transform
        transform.rotation = Quaternion.Euler(cm.xRotation, cm.yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0, cm.yRotation, 0);
    }
}
