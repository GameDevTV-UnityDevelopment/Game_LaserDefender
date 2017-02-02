using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 150f;
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    /// <summary>
    /// Initialisation
    /// </summary>
    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;

        if (Random.value < probability)
        {
            Fire();
        }
    }

    /// <summary>
    /// Fires the enemy lasers
    /// </summary>
    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    /// <summary>
    /// Event handler for collision between enemy and player lasers
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();

        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// Destroys the enemy
    /// </summary>
    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        scoreKeeper.Score(scoreValue);
        Destroy(gameObject);
    }
}
