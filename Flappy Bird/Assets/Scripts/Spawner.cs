using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BirdControl BirdScript;

    public GameObject Borular;

    public float height;

    public float time;


    public void Start()
    {
        StartCoroutine(SpawnObject(time));
    }
    public IEnumerator SpawnObject(float time)
    {
        while (!BirdScript.isDead)
        {
            Instantiate(Borular, new Vector3(1.5f, Random.Range(-height, height), 0), Quaternion.identity);
            

            yield return new WaitForSeconds(time);
        }
    }
}
