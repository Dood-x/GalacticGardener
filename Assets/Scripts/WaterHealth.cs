using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHealth : MonoBehaviour
{
    FuelDraining fuelDraining;

   

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fuelDraining.Regen();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>();
        }
    }

   
}
