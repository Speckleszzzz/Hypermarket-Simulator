using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractions : MonoBehaviour
{

    public void NewGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadGame()
    {

    }

    public void Settings()
    {

    }

    public void QuitGame()
    {

    }
}
