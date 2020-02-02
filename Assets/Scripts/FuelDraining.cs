using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDraining : MonoBehaviour
{
    public Slider FuelBar;
    public float fuel;
    public float MaxFuel;
    float speed = 0.5f;

     void Start()
    {
        fuel = MaxFuel;
        FuelBar = GetComponent<Slider>();
        FuelBar.maxValue = MaxFuel;
        FuelBar.value = fuel;

    }

    void Update()
    {
        fuel -= Time.deltaTime * speed;
        FuelBar.value = fuel;
    }
}
