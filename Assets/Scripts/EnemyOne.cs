using System;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    public GameObject waypoint;
    public float moveSpeed;

    private Rigidbody _rb;
    private Transform[] _wps;
    private Transform _target;
    private int _wpIdx = 1;

    private GameObject _tracker;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _wps = waypoint.GetComponentsInChildren<Transform>();
        _target = _wps[_wpIdx];
        _tracker = new GameObject("_tracker" + GetInstanceID());
        _tracker.transform.position = transform.position;
    }

    private void Update()
    {
        // Move tracker
        if (Vector3.Distance(transform.position, _tracker.transform.position) <= 3f)
        {
            var moveDir = (_target.position - _tracker.transform.position).normalized;
            _tracker.transform.Translate(moveDir * moveSpeed * 2f * Time.deltaTime);
        }
        
        // Move enemy
        var selfMoveDir = (_tracker.transform.position - transform.position).normalized;
        transform.LookAt(_tracker.transform);
        _rb.velocity = selfMoveDir * moveSpeed;

        // Change waypoint index
        if (Vector3.Distance(_target.position, _tracker.transform.position) < 1f)
        {
            _wpIdx = ++_wpIdx % _wps.Length;
            if (_wpIdx == 0) _wpIdx = 1;
            _target = _wps[_wpIdx];
        }
    }
}