using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerBody;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    [SerializeField] private const float landindSlideTime = 0.5f;
    [SerializeField]  private float slideTimer;


    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

   


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerBody.localScale.y;
    }


    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            StartSlide();
        if (Input.GetKeyUp(slideKey) && pm.sliding)
            StopSlide();
        if (pm.dashing == true)
            StopSlide();
        if (!pm.grounded)
            slideTimer = maxSlideTime - landindSlideTime;

    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }


    //мне очень не нравится как сжимается игрок при слайде, так что потенциально нужно будет менять функцию StartSlide
    private void StartSlide()
    {
        pm.sliding = true;

        playerBody.localScale = new Vector3(playerBody.localScale.x, slideYScale, playerBody.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // SLIDING NORMAL
        if(!pm.OnSlope()|| rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;

        }


        // sliding down a slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
        

        if (slideTimer <= 0 && pm.grounded)
            StopSlide();      
    }

    private void StopSlide()
    {

        pm.sliding = false;

        playerBody.localScale = new Vector3(playerBody.localScale.x, startYScale, playerBody.localScale.z);
    }
}
