using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyAfterTime;

    private float _destroyTime;

    private void Start()
    {
        _destroyTime = Time.time + destroyAfterTime;
    }

    private void Update()
    {
        if (Time.time >= _destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
