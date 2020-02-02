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

    public bool corruptable = true;


    void Start()
    {
        adjacentCorruption = new List<Corruption>();
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
        SetCorrupted();
        corruptionSource.AddTree();

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
        FindObjectOfType<AudioManager>().Play("Healed");
        corrupSide.SetActive(false);

        timer = Random.Range(timerMin, timerMax);
        corruptionSource.UpdateHealed();

        GameObject ps = corruptionSource.cm.GetComponent<CorruptionManager>().healedParticle;
        GameObject psinstance = Instantiate(ps, transform.position, transform.rotation);

        StartCoroutine("DestoryHealParticles", psinstance);
    }

    IEnumerator DestoryHealParticles(GameObject ps)
    {
        yield return new WaitForSeconds(3);
        Destroy(ps);
    }

    public void SetCorrupted()
    {
        healed = false;
        corrupSide.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Corrupted");
        healedSide.SetActive(false);
        corruptionSource.UpdateCorrupted();
        ParticleSystem ps = corruptionSource.cm.GetComponent<CorruptionManager>().corruptionParticle;
        Instantiate(ps, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

    }

    void SetActiveChildren(Transform trans, bool active)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.SetActive(active);
        }
    }


    public void Corupt()
    {
        if (!corruptable)
        {
            return;
        }


        timer -= Time.deltaTime;
        if(timer <= 0)
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
                sickboi.Corupt();
            }


        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.tag == "Player" && healed == false /*&& collider.gameObject.GetComponent<CharachterController>().fuel > 0*/ )
        {
            SetHealed();
        }
    }








}

