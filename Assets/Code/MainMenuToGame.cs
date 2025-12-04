using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuToGame : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
