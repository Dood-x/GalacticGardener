using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorruptionManager : MonoBehaviour
{
    int sources;
    int resolvedSources;

    public float recorruptTimeMin = 15f;
    public float recorruptTimeMax = 26f;

    int treeAmount;
    int healed;

    public GameObject tutorialCanvas;
    public GameObject winCanvas;

    public ParticleSystem corruptionParticle;
    public GameObject healedParticle;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("AssT");

        sources = asteroids.Length;

        for(int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].AddComponent<CorruptionSource>();
        }

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        for (int i = 0; i < trees.Length; i++)
        {
            Corruption c = trees[i].AddComponent<Corruption>();
            c.timerMin = recorruptTimeMin;
            c.timerMax = recorruptTimeMax;
        }

        treeAmount = trees.Length;
        healed = treeAmount;
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine("TutorialCanvas");
    }

    IEnumerator TutorialCanvas()
    {
        tutorialCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorialCanvas.SetActive(false);
    }

    IEnumerator Won()
    {
        winCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("EndingScene");
    }

    public void UpdateResolvedSources(CorruptionSource cs)
    {
        resolvedSources++;

        GameObject ps = healedParticle;
        GameObject psinstance = Instantiate(ps, transform.position, transform.rotation);

        StartCoroutine("DestoryHealParticles", psinstance);
        Destroy(cs.gameObject);


        if (resolvedSources == sources)
        {
            // GAME WON
            Debug.Log("GAME WON");

            StartCoroutine("Won");


        }
    }

 

    IEnumerator DestoryHealParticles(GameObject ps)
    {
        yield return new WaitForSeconds(3);
        Destroy(ps);
        
    }

    public void UpdateHealed()
    {
        healed++;
        Debug.Log(((float)healed) / treeAmount);
    }

    public void UpdateCorrupted()
    {
        healed--;
        Debug.Log(((float)healed) / treeAmount);
    }

}
