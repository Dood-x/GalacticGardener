using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSource : MonoBehaviour
{
    public List<Corruption> allcorupted;

    public List<Corruption> adjacent;

    public float radius;

    public float regenTime;
    
    public int corruptionStartRadius = 10;

    void Start()
    {
        // find adjacent
        GameObject[] corruptions = GameObject.FindGameObjectsWithTag("Tree");

        for (int i = 0; i < corruptions.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, corruptions[i].transform.position);

            if (distance <= radius)
            {
                adjacent.Add(corruptions[i].GetComponent<Corruption>());
            }

            if (distance <= corruptionStartRadius)
            {
                corruptions[i].GetComponent<Corruption>().SetCorrupted();
            }
        }



    }

    void Update()
    {
        if (IsAllMissionComplete())
        {
            //turn off things
        }
    }

    private bool IsAllMissionComplete()
    {
        for (int i = 0; i < allcorupted.Count; ++i)
        {
            if (allcorupted[i].healed == false)
            {
                return false;
            }
        }

        return true;
    }






}
