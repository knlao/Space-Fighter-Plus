using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTwo : MonoBehaviour
{
    public GameObject cannonBall;
    public float fireRate;
    public bool doBulletFollow;

    private float _nextShootTime;

    private void Start()
    {
        doBulletFollow = Random.Range(0, 1f) <= 0.6f;
    }

    private void Update()
    {
        if (Time.time > _nextShootTime)
        {
            var c = Instantiate(cannonBall, transform.position, cannonBall.transform.rotation);
            c.GetComponent<Cannon>().isWhite = GetComponent<Enemy>().isWhite;
            c.GetComponent<Cannon>().canDmg = true;
            c.GetComponent<Cannon>().doFollow = doBulletFollow;
            _nextShootTime = Time.time + fireRate;
        }
    }
}