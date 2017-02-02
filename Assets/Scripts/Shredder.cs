using UnityEngine;

public class Shredder : MonoBehaviour
{
    /// <summary>
    /// Event handler for collision between shredder and lasers
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
