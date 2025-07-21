using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinMovement : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;

    public float flySpeed;
    public float moveSpeed;
    public float rotationSpeed;
    public float desiredMoveSpeed;
    private float speedChangeFactor;
    private float lastDesiredMoveSpeed;

    public float momentumAfterStop;

    public Vector3 moveDirection;
    public Transform orientation;

    public FuelBasedMovement fbm;

    public float maxFuel;
    public float currentFuel;
    public float remainingFuel;

    private bool keepMomentum  = true;
    private bool crousingCheck;

    void Start()
    {
        currentFuel = maxFuel;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        PlayerInput();

        if (verticalInput > 0)
            verticalInput -= 0.000001f;
    }

    // Update is called once per frame
    void Update()
    {
        crousingCheck = fbm.crousingCheck;

        SpeedHandler();

        if (remainingFuel < currentFuel && remainingFuel != 0)
            currentFuel = remainingFuel;

    }

    private void PlayerInput()
    {
       
        if (currentFuel >= 0)
        {
            verticalInput = Input.GetAxis("Vertical");
            verticalInput = Mathf.Clamp01(verticalInput);

            horizontalInput = Input.GetAxis("Horizontal");

            if (verticalInput > 0)
            {
                currentFuel -= Time.deltaTime;
                if (desiredMoveSpeed < flySpeed)
                    desiredMoveSpeed += 0.03f;
            }

            else if (verticalInput <= 0 && desiredMoveSpeed > 0)
            {
                desiredMoveSpeed -=  Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S) && desiredMoveSpeed > 0)
                desiredMoveSpeed -= Time.deltaTime * 2;
        }

    }


    public void SpeedHandler()
    {



        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 9f)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());

        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 0.01f;
        keepMomentum = false;

}


    public void MovePlayer()
    {
        if (verticalInput > 0)
        { 

            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            transform.Translate(moveDirection.normalized * desiredMoveSpeed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero && verticalInput != 0)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            }
        }

        //else if (fbm.moveSpeed != 0 && momentumAfterStop > 0)
        //{

        //        moveDirection = orientation.forward + orientation.right * 0;

        //        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        //}




    }

}