using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTree : MonoBehaviour
{

    public Sprite Tree;
    public Sprite CorruptTree;
    public bool heal;
    public bool cantpress = true;
    public float spawnTimer = 7f;


    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CorruptTree;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<CharachterController>().fuel > 0)
        {
            heal = true;
            GetComponent<SpriteRenderer>().sprite = Tree;
            cantpress = false;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTimer);

        heal= false;
        GetComponent<SpriteRenderer>().sprite= CorruptTree;
        cantpress = true;
    }

}
