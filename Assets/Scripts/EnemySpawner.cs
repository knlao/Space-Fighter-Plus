using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyFormations;
    public Vector2 spawnRate;

    private float _nextSpawnTime;

    private void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            Instantiate(enemyFormations[Random.Range(0, enemyFormations.Length)]);
            _nextSpawnTime = Time.time + Random.Range(spawnRate.x, spawnRate.y);
        }
    }
}
