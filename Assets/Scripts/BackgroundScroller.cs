using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    float backgroundScrollSpeed = 0.5f;

    private Material background;
    private Vector2 offset;

    void Start()
    {
        background = GetComponent<Renderer>().material;

        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    void Update()
    {
        background.mainTextureOffset += offset * Time.deltaTime;
    }
}