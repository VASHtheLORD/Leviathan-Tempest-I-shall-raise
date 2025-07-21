using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotPosition : MonoBehaviour
{
    public PlayerMovement pm;
    public Transform pilotPosition;
    public GameObject Player;    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.enabled == false)
            Player.transform.position = pilotPosition.position;
    }
}
