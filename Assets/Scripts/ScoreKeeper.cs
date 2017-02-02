using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int score = 0;
    private Text myText;

    /// <summary>
    /// Initialisation
    /// </summary>
    private void Start()
    {
        myText = gameObject.GetComponent<Text>();
        Reset();
    }

    /// <summary>
    /// Adds the specified number of points to the score
    /// </summary>
    /// <param name="points">The points to be added</param>
    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    /// <summary>
    /// Resets the score to zero
    /// </summary>
    public static void Reset()
    {
        score = 0;
    }
}
