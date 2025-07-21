using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour, IInteractable
{
    public PlayerMovement pm;
    public ZeppelinMovement zm;

    private bool playerIsActive = true;



    void Update()
    {
    }

    public void Interact()
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
