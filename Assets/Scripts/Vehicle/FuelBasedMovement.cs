using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FuelBasedMovement : MonoBehaviour
{
    public ZeppelinMovement zm;
    public float remainingFuel = 0;
    public float moveSpeed;

    public float lastHorizontalInput;
    public float lastVerticalInput;

    public bool crousingCheck;

    public Vector3 moveDirection;
    public Transform orientation;

    public AudioSource au;

    private void Start()
    {
        zm = GetComponent<ZeppelinMovement>();

    }

    public void FixedUpdate()
    {
        lastHorizontalInput = zm.horizontalInput;
        lastVerticalInput = zm.verticalInput;


        if (remainingFuel > 0 && zm.enabled == false)
        {
            moveSpeed = zm.moveSpeed;

            CrousingForward();

            crousingCheck = true;
        }

        else if (remainingFuel > 0 && lastVerticalInput <= 0 && moveSpeed != 0)
        {

            moveSpeed = zm.moveSpeed;

            SmoothStop();

            //zm.momentumAfterStop -= Time.deltaTime;

        }

        else if (remainingFuel <= 0 && zm.enabled == false)
        {
            moveSpeed = 0;
        }

        if (zm.enabled == true)
        {
            crousingCheck = false;

            zm.momentumAfterStop = 10;
        }

    }


    private void Update()
    {
        moveSpeed = zm.moveSpeed;

        if (zm.enabled == false)
            zm.remainingFuel = remainingFuel;

        if (zm.enabled == true)
            remainingFuel = zm.currentFuel;


    }


    private void CrousingForward()
    {
        if (lastVerticalInput != 0 && zm.moveSpeed > 0)
            remainingFuel -= Time.deltaTime;


        moveDirection = orientation.forward * lastVerticalInput;

        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (zm.desiredMoveSpeed > 0 && lastVerticalInput <= 0)
            SmoothStop();

    }

    private void SmoothStop()
    {

        zm.desiredMoveSpeed -= Time.deltaTime;

        if (zm.desiredMoveSpeed < 0)
            zm.desiredMoveSpeed = 0;

        remainingFuel -= Time.deltaTime;

        moveDirection = orientation.forward + orientation.right * 0;

        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

}

