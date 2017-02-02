using UnityEngine;

public class Position : MonoBehaviour
{
    /// <summary>
    /// Draws a wire sphere gizmo to indicate formation position
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
