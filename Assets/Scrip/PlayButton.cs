
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [Header("Pengaturan Scene")]
    public string sceneName = "Space";

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
