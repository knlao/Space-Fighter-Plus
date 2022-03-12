using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Gradient whiteColor;
    public Gradient blackColor;
    public Gradient readyWhiteColor;
    public Gradient readyBlackColor;
    public bool isWhite;
    public GameObject line;
    public float animTime;
    public float duration;

    private float _boomTime;
    private float _dieTime;
    private Animator _anim;
    private BoxCollider _bc;
    private AudioSource _au;

    private void Start()
    {
        _bc = GetComponent<BoxCollider>();
        _bc.enabled = false;
        _anim = GetComponent<Animator>();
        _anim.SetBool("isReady", false);
        line.GetComponent<LineRenderer>().colorGradient = (isWhite ? readyWhiteColor : readyBlackColor);
        _boomTime = Time.time + animTime;
        _dieTime = _boomTime + duration;
        _au = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time > _boomTime)
        {
            line.GetComponent<LineRenderer>().colorGradient = (isWhite ? whiteColor : blackColor);
            _bc.enabled = true;
            _anim.SetBool("isReady", true);
        }

        _au.pitch = Time.timeScale;

        if (Time.time > _dieTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().isWhite != isWhite)
        {
            other.gameObject.GetComponent<PlayerController>().Dies();
        }
    }
}
