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
    [SerializeField] bool _isLevel1 = false;
    [SerializeField] GameObject turtle;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_isLevel1) return;
        GameObject newEnemy = Instantiate(enemy, transform.position,
            transform.rotation) as GameObject;

    }

    public void InstantiateOneEnemy(GameObject enemy)
    {        
         Instantiate(enemy, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("SpawnTheEnemy!");
            turtle.SetActive(true);
            
            
        }
    }
}
