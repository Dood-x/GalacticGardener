using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FuelDraining : MonoBehaviour
{
    public Slider fuelBar;
    public float fuel;
    public float maxFuel;
    public  float speed = 0.5f;
    public bool regenHealth = false;

     void Start()
    {
        fuel = maxFuel;
        fuelBar = fuelBar.GetComponent<Slider>();
        fuelBar.maxValue = maxFuel;
        fuelBar.value = fuel;

    }

    void Update()
    {
        // uvjet ako regenamo onda return

        if (regenHealth == true)
        {
            return;
        }
        fuel -= Time.deltaTime * speed;
        fuelBar.value = fuel;

        //game over

    }

    public void Regen()
    {
        fuel += Time.deltaTime * speed;
        fuel = Mathf.Clamp(fuel, 0f, maxFuel);
    }
}
