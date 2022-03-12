using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public bool isWhite;
    public bool canDmg;
    public bool doFollow;
    public GameObject explosion;
    public Material whiteMat;
    public Material blackMat;
    public Gradient whiteParticle;
    public Gradient blackParticle;
    public GameObject model;
    public GameObject particle;

    private Rigidbody _rb;
    private Vector3 _selfMoveDir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerController>().gameObject;
        if (!doFollow)
        {
            transform.LookAt(target.transform.position);
            _selfMoveDir = (target.transform.position - transform.position).normalized;
        }

        model.GetComponent<MeshRenderer>().material = (isWhite ? whiteMat : blackMat);
        var settings = particle.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient((isWhite ? whiteParticle : blackParticle));
    }

    private void Update()
    {
        if (doFollow)
        {
            _selfMoveDir = (target.transform.position - transform.position).normalized;
            transform.LookAt(target.transform.position);
        }

        _rb.velocity = _selfMoveDir * speed;

        model.GetComponent<MeshRenderer>().material = (isWhite ? whiteMat : blackMat);
        var settings = particle.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient((isWhite ? whiteParticle : blackParticle));

        // Border Limit
        if (Mathf.Abs(transform.position.y) >= 40f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDmg)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if (isWhite == other.gameObject.GetComponent<PlayerController>().isWhite)
                {
                    other.gameObject.GetComponent<PlayerController>().Absorb();
                    Destroy(gameObject);
                }
                else
                {
                    other.gameObject.GetComponent<PlayerController>().Dies();
                    Instantiate(explosion, transform.position, explosion.transform.rotation);
                    Destroy(gameObject);
                }
            }


            if (other.gameObject.CompareTag("Player Bullet"))
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Player Super Bullet"))
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}