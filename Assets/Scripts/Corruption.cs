using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{

    public List<Corruption> adjacentCorruption;

    public CorruptionSource corruptionSource;


    public bool healed;

    float timer;
    public float timerMin = 4f;
    public float timerMax = 6f;

    public float spawnTimer = 7f;

    public bool cantpress = true;

    public bool checkList;

    public float radius = 3f;

    GameObject corrupSide;
    GameObject healedSide;

    bool firstFrame = true;


    private void Start()
    {
        GameObject[] sources = GameObject.FindGameObjectsWithTag("AssT");

        float dist = int.MaxValue;
        int index = 0;

        for (int i = 0; i < sources.Length; i++)
        {
            float distance = Vector3.Distance(sources[i].transform.position, transform.position);
            if (distance < dist)
            {
                index = i;
                dist = distance;
            }
        }

        corruptionSource = sources[index].GetComponent<CorruptionSource>();

        corruptionSource.allcorupted.Add(this);


        //find adjacent
        GameObject[] corruptions = GameObject.FindGameObjectsWithTag("Tree");

        for (int i = 0; i < corruptions.Length; i++)
        {
            
            float distance = Vector3.Distance(transform.position, corruptions[i].transform.position);

            if (distance <= radius)
            {
                Corruption c = corruptions[i].GetComponent<Corruption>();
                adjacentCorruption.Add(c);
            }
        }

        // get corrupt and healed versions

        foreach (Transform child in transform)
        {
            if(child.tag == "CTree")
            {
                corrupSide = child.gameObject;
            }
            if (child.tag == "GTree")
            {
                healedSide = child.gameObject;
            }
        }

        //set as healed
        SetHealed();


    }

    void Update()
    {
        if (firstFrame)
        {
            PurgeAdjacent();
            firstFrame = false;
        }
        CheckAdjacent();
    }

    public void SetHealed()
    {
        healed = true;
        healedSide.SetActive(true);
        corrupSide.SetActive(false);

        timer = Random.Range(timerMin, timerMax);
    }

    public void SetCorrupted()
    {
        healed = false;
        corrupSide.SetActive(false);
        healedSide.SetActive(true);
    }

    
    public void Corupt()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            SetCorrupted();
        }
        
    }
    public void PurgeAdjacent()
    {
        for (int i = adjacentCorruption.Count - 1; i >= 0; i--)
        {
            if (adjacentCorruption[i].corruptionSource != corruptionSource)
            {
                adjacentCorruption.RemoveAt(i);
            }
        }
    }

    public void CheckAdjacent()
    {

        foreach (Corruption sickboi in adjacentCorruption)
        {
            if (sickboi.healed == true)
            {
                Corupt();
            }


        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.tag == "Player"  && collider.gameObject.GetComponent<CharachterController>().fuel > 0 )
        {
            SetHealed();
        }
    }








}

