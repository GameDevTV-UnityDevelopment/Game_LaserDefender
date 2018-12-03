using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Player player;
    private Text healthText;


    void Start()
    {
        player = FindObjectOfType<Player>();

        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}