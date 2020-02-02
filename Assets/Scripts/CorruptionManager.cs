using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("AssT");

        for(int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].AddComponent<CorruptionSource>();
        }

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].AddComponent<Corruption>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if all asteroids are off the game is won
    }
}
