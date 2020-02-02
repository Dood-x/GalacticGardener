using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fuel;
    float fuel_Start;
    public float fuel_Current;

    public float fuelReg;

    void Start()
    {
        fuel_Start = fuel;
        fuel_Current = fuel;
    }

    void Update()
    {
        if (fuel_Current < fuel_Start)
        {
            fuel_Current -= fuelReg * Time.deltaTime;
        }
        if (fuel_Current <= 0)
        {
            Application.Quit();
            Debug.Log("The planet was lost!");
        }
    }
}
