using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawn = 1f;
    [SerializeField] float maxSpawn = 5f;
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy, transform.position,
            transform.rotation) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
