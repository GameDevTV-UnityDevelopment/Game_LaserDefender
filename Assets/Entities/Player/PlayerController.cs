using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
    public float padding = 1f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float firingRate = 0.2f;
    public float health = 250f;
    public AudioClip fireSound;

    private float xMin;
    private float xMax;

    /// <summary>
    /// Initialisation
    /// </summary>
    private void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));

        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Fires the player's laser
    /// </summary>
    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    /// <summary>
    /// Destroys the player
    /// </summary>
    private void Die()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("Win Screen");

        Destroy(gameObject);
    }

    /// <summary>
    /// Event handler for collision between player and the enemy lasers
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
}