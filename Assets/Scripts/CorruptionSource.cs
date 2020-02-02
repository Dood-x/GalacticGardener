using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSource : MonoBehaviour
{
    public List<Corruption> allcorupted;

    public List<Corruption> adjacent;

    public float radius;

    public float regenTime;
    
    public float corruptionStartRadius = 0.1f;

    public bool resolved = false;

    public GameObject cm;

    int trees;
    int healedTrees;

    void Start()
    {

        allcorupted = new List<Corruption>();
        adjacent = new List<Corruption>();
        // find adjacent
        GameObject[] corruptions = GameObject.FindGameObjectsWithTag("Tree");

        for (int i = 0; i < corruptions.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, corruptions[i].transform.position);

            if (distance <= radius)
            {
                adjacent.Add(corruptions[i].GetComponent<Corruption>());
            }

            //if (distance <= corruptionStartRadius)
            //{
            //    corruptions[i].GetComponent<Corruption>().SetCorrupted();
            //}
        }

        cm = GameObject.FindGameObjectWithTag("CorruptionManager");

    }
    void Update()
    {
        CheckAdjacent();
    }

    //private bool IsAllMissionComplete()
    //{
    //    for (int i = 0; i < allcorupted.Count; ++i)
    //    {
    //        if (allcorupted[i].healed == false)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    public void UpdateHealed()
    {
        healedTrees++;
        cm.GetComponent<CorruptionManager>().UpdateHealed();
        if (healedTrees == trees)
        {
            //turn off things
            for (int i = 0; i < allcorupted.Count; ++i)
            {

                allcorupted[i].corruptable = false;
            }

            cm.GetComponent<CorruptionManager>().UpdateResolvedSources(this);

            resolved = true;

            Debug.Log("Resolved Source");

        }
    }

  

    public void UpdateCorrupted()
    {
        healedTrees--;
        cm.GetComponent<CorruptionManager>().UpdateCorrupted();
    }

    public void AddTree()
    {
        trees++;
        healedTrees++;
    }


    public void CheckAdjacent()
    {

        foreach (Corruption sickboi in adjacent)
        {
            if (sickboi.healed == true)
            {
                sickboi.Corupt();
            }


        }
    }

}
