using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public float Timer = 0f;
    public float maxTime = 5f;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(-6, 6), 10, Random.Range(-6, 6));
            Instantiate(boxPrefab, randomSpawnPos, Quaternion.identity);
            Timer = 0f;
            maxTime += 0.5f;
        }
    }
}
