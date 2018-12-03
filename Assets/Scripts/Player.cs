using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private int health = 200;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float padding = 1f;

    [SerializeField]
    private AudioClip deathSFX;

    [SerializeField]
    [Range(0f, 1f)]
    private float deathSFXVolume = 0.7f;

    [Header("Projectile")]
    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private float projectileFiringPeriod = 0.1f;

    [SerializeField]
    private AudioClip laserSFX;

    [SerializeField]
    [Range(0f, 1f)]
    private float laserSFXVolume = 0.25f;

    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;

    private Coroutine firingCoroutine;


    public int GetHealth()
    {
        return health;
    }

    private void Start()
    {
        SetUpMovementBoundaries();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);

            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;

        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - padding;
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
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        FindObjectOfType<Level>().LoadGameOver();

        Destroy(gameObject);
    }
}