using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnterRegularTower : MonoBehaviour
{
    public AudioSource au;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Player")
            {
            au.enabled = true;
            }
    }
}
