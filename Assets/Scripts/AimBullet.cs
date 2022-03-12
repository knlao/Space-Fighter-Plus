using System;
using UnityEngine;

public class AimBullet : MonoBehaviour
{
    public Gradient white;
    public Gradient black;
    public bool isWhite;
    public float speed;
    public Transform[] wps;

    private Rigidbody _rb;
    private TrailRenderer _tr;

    // private Transform _target;
    // private int _wpIdx;
    private Transform _finalTarget;
    // private GameObject _tracker;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponentInChildren<TrailRenderer>();
        _tr.colorGradient = (isWhite ? white : black);
        // _target = wps[_wpIdx];
        _finalTarget = wps[wps.Length-1];
        // _tracker = new GameObject("_tracker" + GetInstanceID());
    }

    public void Update()
    {
        if (_finalTarget == null)
        {
            Destroy(gameObject);
        }

        // // Move tracker
        // print(_tracker.transform.position);
        //
        // if (Vector3.Distance(transform.position, _tracker.transform.position) <= 3f)
        // {
        //     var moveDir = (_target.position - _tracker.transform.position).normalized;
        //     _tracker.transform.Translate(moveDir * speed * 2f * Time.deltaTime);
        // }
        //
        // // Move bullet
        // var selfMoveDir = (_tracker.transform.position - transform.position).normalized;
        // transform.LookAt(_tracker.transform);
        // _rb.velocity = selfMoveDir * speed;
        //
        // // Change waypoint index
        // if (Vector3.Distance(_target.position, _tracker.transform.position) < 1f)
        // {
        //     _wpIdx = ++_wpIdx % wps.Length;
        //     if (_wpIdx == 0) _wpIdx = 1;
        //     _target = wps[_wpIdx];
        // }
        
        // Movement
        transform.LookAt(_finalTarget);
        transform.position = Vector3.MoveTowards(transform.position, _finalTarget.position, speed * Time.deltaTime);
        
        // Border Limit
        if (Mathf.Abs(transform.position.y) >= 40f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_finalTarget.gameObject == other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}