using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{

    public List<Corruption> adjustCorruption;

    public CorruptionSource corruptionSource;


    public bool healed;

    float timer = 5f;

    public float spawnTimer = 7f;

    public bool cantpress = true;

    public bool checkList;

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && cantpress == true)
        {
            healed = true;
            Debug.Log("healan je");
            GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("sakrio se");
            cantpress = false;
            StartCoroutine(Spawner());
           
        }
       
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnTimer);

        healed = false;
        GetComponent<MeshRenderer>().enabled = true;
        cantpress = true;        
    }
    public void Corupt()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            healed = false;
            Debug.Log("nije healan");
        }
        
    }

    void CheckAdjacent()
    {
        for(int i = 0; i <adjustCorruption.Count; i++)
        {
            if(adjustCorruption[i].healed == false)
            {
                Corupt();
            }
            else if(adjustCorruption[i].healed == true)
            {
                Debug.Log("win game");
            }
        }

        //foreach(Corruption sickboi in adjustCorruption)
        //{
        //    if(sickboi.healed == true)
        //    {
        //        Corupt();
        //    }
            

        //}
    }

     void OnTriggerEnter(Collider collider)
    {


        if(collider.gameObject.tag == "Player"  && collider.gameObject.GetComponent<CharachterController>().fuel > 0 )
        {
            healed = true;
            GetComponent<MeshRenderer>().enabled = false;           
            cantpress = false;
            StartCoroutine(Spawner());


        }
    }








}

