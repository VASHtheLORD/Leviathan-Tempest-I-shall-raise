using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerRotationScript : MonoBehaviour
{
    public ZeppelinMovement zm;
    [SerializeField]  private Vector3 _rotation;

    private float horizontalInput;
    private float verticalInput;

    

    void Start()
    {
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (zm.enabled == true)
            verticalInput = Mathf.Clamp01(verticalInput);
    }

    void Update()
    {
        if (zm.enabled == true)
        {
            if (horizontalInput > 0 && verticalInput != 0)
                transform.Rotate(_rotation * Time.deltaTime, Space.Self);
            else if (horizontalInput < 0 && verticalInput != 0)
                transform.Rotate(-_rotation * Time.deltaTime, Space.Self);
        }

        

    }


}
