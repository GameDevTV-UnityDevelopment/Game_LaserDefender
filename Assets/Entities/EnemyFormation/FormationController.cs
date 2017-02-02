using UnityEngine;

public class FormationController : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float spawnDelay = 0.25f;

    private bool movingRight = false;

    private float xMin;
    private float xMax;

    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera));

        xMin = leftBoundary.x;
        xMax = rightBoundary.x;

        SpawnUntilFull();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }

    /// <summary>
    /// Spawns enemies until each enemy position in the formation is filled
    /// </summary>
    private void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();

        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    /// <summary>
    /// Returns the position of the next empty formation position
    /// </summary>
    private Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Indicates whether all enemies have been destroyed
    /// </summary>
    private bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Draws a wire cube gizmo to indicate position of enemy
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
