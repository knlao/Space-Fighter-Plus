using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool isWhite;
    public GameObject explosion;
    public float health;
    public float sameColorDmg;
    public float diffColorDmg;
    public bool canBeAbsorbed;
    public GameObject healthBar;

    private void Start()
    {
        healthBar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthBar.GetComponent<HealthBar>().SetHealth(health);
        healthBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Hit by player
        if (other.gameObject.CompareTag("Player"))
        {
            if (canBeAbsorbed && isWhite == other.gameObject.GetComponent<PlayerController>().isWhite)
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
        
        // Get hit by same diff player's bullet
        if (other.gameObject.CompareTag("Player Bullet") &&
            other.gameObject.GetComponent<Bullet>().isWhite != isWhite)
        {
            health -= diffColorDmg;
            healthBar.GetComponent<HealthBar>().SetHealth(health);
            healthBar.SetActive(true);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Dies();
            }
        }

        // Get hit by same color player's bullet
        if (other.gameObject.CompareTag("Player Bullet") &&
            other.gameObject.GetComponent<Bullet>().isWhite == isWhite)
        {
            health -= sameColorDmg;
            healthBar.GetComponent<HealthBar>().SetHealth(health);
            healthBar.SetActive(true);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Dies();
            }
        }

        // Get hit by player's super bullet
        if (other.gameObject.CompareTag("Player Super Bullet"))
        {
            Dies();
        }
    }

    public void Dies()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        FindObjectOfType<GameManager>().AddScore(1000);
        FindObjectOfType<GameManager>().AddSubScore(200);
        FindObjectOfType<SFX>().PlaySFX(1);
        Destroy(gameObject);
    }
}