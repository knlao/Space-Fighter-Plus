using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _cam;

    private void Start()
    {
        _cam = FindObjectOfType<Camera>().gameObject.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.forward);
    }
}
