using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudManager : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] GameObject[] clouds;
    private float nextSpawnTime;
    private Vector3 position = new Vector3(-350f, 60f, 0f);


    void Start() //populate script
    {
        int j = Random.Range(27, 34);

        for (int i = 0; i < j; i++)
        {
            position.z = Random.Range(-200, 200);
            position.x = Random.Range(-200, 200);
            Instantiate(clouds[Random.Range(0, 6)], position, Quaternion.Euler(0, 90, 0));
        }

        position.x = -200;
    }


    void Update()
    {
        if (Time.time < nextSpawnTime)
            return;

        position.z = Random.Range(-200, 200);

        Instantiate(clouds[Random.Range(0, 6)], position, Quaternion.Euler(0, 90, 0));


        nextSpawnTime = Time.time + spawnRate;
    }
}
