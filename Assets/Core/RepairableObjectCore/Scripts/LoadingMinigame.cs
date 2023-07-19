using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMinigame : MonoBehaviour
{
    [SerializeField] private SceneField _minigameScene;

    public void LoadMinigame()
    {
        SceneManager.LoadScene(_minigameScene.Name, LoadSceneMode.Additive);
    }

    public void UnloadMinigame()
    {
        SceneManager.UnloadSceneAsync(_minigameScene.Name);
    }
}
