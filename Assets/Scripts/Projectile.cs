using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 100f;

    /// <summary>
    /// Returns the damage the projectile causes
    /// </summary>
    /// <returns>Float</returns>
    public float GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// Destroys the projectile
    /// </summary>
    public void Hit()
    {
        Destroy(gameObject);
    }
}
