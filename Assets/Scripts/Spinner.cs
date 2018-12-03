using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    private float spin = 1f;


    void Update()
    {
        transform.Rotate(0f, 0f, spin * Time.deltaTime);
    }
}