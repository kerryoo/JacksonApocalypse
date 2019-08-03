using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> ZombiePrefabs;

    public int maximumUnits;
    public int waveNumber;
    public readonly int minX = -13;
    public readonly int minZ = -36;
    public readonly int maxX = 46;
    public readonly int maxZ = 20;
    public readonly int minY = 11;

    private void Start()
    {
        maximumUnits = 2;
        StartCoroutine(WaveTimer());
    }

    void Update()
    {
        
    }

    private IEnumerator WaveTimer()
    {
        waveNumber++;
        yield return new WaitForSeconds(120f);
        maximumUnits *= waveNumber;

        for (int i = 0; i < maximumUnits; i ++)
        {
            Instantiate(ZombiePrefabs[0], new Vector3(Random.Range(minX, maxX), minY, Random.Range(minZ, maxZ)), Quaternion.identity);
        }


        StartCoroutine(WaveTimer());
    }
}
