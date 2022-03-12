using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gradient white;
    public Gradient black;
    public bool isWhite;
    public float speed;

    private Rigidbody _rb;
    private TrailRenderer _tr;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponentInChildren<TrailRenderer>();
        _tr.colorGradient = (isWhite ? white : black);
    }

    public void Update()
    {
        // Movement
        _rb.velocity = Vector3.up * speed;
        
        // Border Limit
        if (Mathf.Abs(transform.position.y) >= 40f)
        {
            Destroy(this.gameObject);
        }
    }
}
