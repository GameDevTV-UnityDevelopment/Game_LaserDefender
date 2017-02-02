using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
    }
}
