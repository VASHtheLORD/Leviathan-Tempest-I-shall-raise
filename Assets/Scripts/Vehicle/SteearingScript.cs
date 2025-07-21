using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteearingScript : MonoBehaviour, IInteractable
{

    public PlayerMovement pm;
    public ZeppelinMovement zm;

    private bool playerIsActive = true;

    void Start()
    {
        zm.enabled = false;
    }



    void Update()
    {
       
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerIsActive == true)
            {
                pm.enabled = false;
                zm.enabled = true;
                playerIsActive = false;

            }

            else
            {
                pm.enabled = true;
                zm.enabled = false;
                playerIsActive = true;

            }
       }

    }

}
