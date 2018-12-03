using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float projectileSpeed = 10f;

    private float shotCounter;

    [SerializeField]
    private float minTimeBetweenShots = 0.2f;

    [SerializeField]
    private float maxTimeBetweenShots = 3f;

    [Header("Sound FX")]
    [SerializeField]
    private AudioClip laserSFX;

    [SerializeField]
    [Range(0f, 1f)]
    private float laserSFXVolume = 0.5f;

    [SerializeField]
    private GameObject explosionVFXPrefab;

    [SerializeField]
    private AudioClip deathSFX;

    [SerializeField]
    [Range(0f, 1f)]
    private float deathSFXVolume = 0.7f;


    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0f)
        {
            Fire();

            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);

        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);

        Instantiate(explosionVFXPrefab, transform.position, transform.rotation);

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        Destroy(gameObject);
    }
}