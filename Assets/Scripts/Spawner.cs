using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    public float startTimeBtwSpawns = 3.5f;
    private float timeBtwSpawns;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    private void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            rand = Random.Range(0, enemies.Length);
            randPosition = Random.Range(0, spawnPoint.Length);
            if (GameObject.Find("Player").transform.position.x - spawnPoint[randPosition].position.x <= 4f)
            {
                if (randPosition == 0)
                {
                    randPosition++;
                }
                else
                {
                    randPosition--;
                }
            }
            if (StaticStats.numEnemies < 25)
            {
                Instantiate(enemies[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
                StaticStats.numEnemies++;
            }
            
            timeBtwSpawns = (startTimeBtwSpawns + (2*StaticStats.numEnemies/3f) / System.Convert.ToSingle(StaticStats.difficulty));
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
