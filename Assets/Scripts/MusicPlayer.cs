using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    /// <summary>
    /// Pre-Initialisation
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }

        SceneManager.sceneLoaded += LevelChange;
    }

    /// <summary>
    /// Delegate method to handle music change on level load
    /// </summary>
    /// <param name="scene">The scene</param>
    /// <param name="mode">The load scene mode</param>
    private void LevelChange(Scene scene, LoadSceneMode mode)
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        music.Stop();

        if (level == 0)
        {
            music.clip = startClip;
        }
        else if (level == 1)
        {
            music.clip = gameClip;
        }
        else if (level == 2)
        {
            music.clip = endClip;
        }

        music.Play();
        music.loop = true;
    }
}