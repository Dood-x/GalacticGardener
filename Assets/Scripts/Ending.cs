using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    public float endingDuration = 3f;
    public string sceneName = "MainMenu";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("End");
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(endingDuration);
        SceneManager.LoadScene(sceneName);
    }

}
