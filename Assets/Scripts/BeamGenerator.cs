using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeamGenerator : MonoBehaviour
{
    public Vector2 beamPositions;
    public GameObject beam;
    public Vector2 spawnRate;
    public Vector2 spawnAmount;

    private float _nextBoomTime;

    private void Start()
    {
        _nextBoomTime = Time.time + Random.Range(spawnRate.x, spawnRate.y);
    }

    private void Update()
    {
        if (Time.time > _nextBoomTime)
        {
            for (var i = 0; i < Random.Range(spawnAmount.x, spawnAmount.y); i++)
            {
                var p = Random.Range(beamPositions.x, beamPositions.y);
                            var b = Instantiate(beam, 
                                new Vector3(0, p, 0), new Quaternion(0, 0, 0, 0));
                            b.GetComponent<LaserBeam>().isWhite = Random.Range(0, 2) == 0;
            }
            _nextBoomTime = Time.time + Random.Range(spawnRate.x, spawnRate.y);
        }
    }
}
