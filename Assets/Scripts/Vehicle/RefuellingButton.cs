using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuellingButton : MonoBehaviour, IInteractable
{
    public ZeppelinMovement zm;
    public FuelBasedMovement fbm;
    public float currentFuel;


    [SerializeField] private Vector3 _rotation;


    void Start()
    {

    }


    void Update()
    {
        FuelCheck();
    }

    public void Interact()
    {
        if (currentFuel < zm.maxFuel)
        {
            currentFuel += Time.deltaTime * 6f;
            fbm.remainingFuel = currentFuel;
            transform.Rotate(_rotation * Time.deltaTime, Space.Self);
        }
            
    }

    public void FuelCheck()
    {
            currentFuel = fbm.remainingFuel;
    }
}
