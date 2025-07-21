using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaverRotationScript : MonoBehaviour
{
    public ZeppelinMovement zm;
    [SerializeField] private Vector3 _rotation;

    private float verticalInput;
    private float xRotation;


    void Start()
    {

    }

    private void FixedUpdate()
    {

        verticalInput = Input.GetAxis("Vertical");
        if (zm.enabled == true)
            verticalInput = Mathf.Clamp01(verticalInput);
    }

    void Update()
    {


        if (zm.enabled == true)
        {
            if (verticalInput == 0  && transform.rotation.x >= -90 && transform.rotation.x <= -55 )
                transform.Rotate(-_rotation * Time.deltaTime, Space.Self);
            else if (verticalInput > 0 && transform.rotation.x >= -90 && transform.rotation.x <= -55)
                transform.Rotate(_rotation * Time.deltaTime, Space.Self);
        }



    }
}
