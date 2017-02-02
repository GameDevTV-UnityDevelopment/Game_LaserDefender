using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Loads the level with the specified name
    /// </summary>
    /// <param name="name">The name of the level to load</param>
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Loads the next level in the scene build index order
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    /// <remarks>Limited functionality on Web and Debug/Editor builds, bad practice for mobile devices</remarks>
    public void QuitRequest()
    {
        Application.Quit();
    }
}