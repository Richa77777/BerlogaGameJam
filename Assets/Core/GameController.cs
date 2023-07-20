using System;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public Action<string> MinigameCompleted; // string - Scene.name

    public Device CurrentRotateableObject { get; private set; }
    public string CurrentMinigameName { get; private set; } = string.Empty;

    public void SetCurrentRotateableObject(Device rotateableObject)
    {
        if (CurrentRotateableObject == null)
        {
            CurrentRotateableObject = rotateableObject;
        }
    }

    public void LoadMinigame(string minigameSceneName)
    {
        if (CurrentMinigameName == string.Empty)
        {
            SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);

            CurrentRotateableObject.gameObject.SetActive(false);
            CurrentMinigameName = minigameSceneName;

            MinigameCompleted += UnloadMinigame;
        }
    }

    public void UnloadMinigame(string minigameSceneName)
    {
        if (CurrentMinigameName != string.Empty)
        {
            CurrentRotateableObject.gameObject.SetActive(true);
            CurrentMinigameName = string.Empty;

            SceneManager.UnloadSceneAsync(minigameSceneName);

            MinigameCompleted -= UnloadMinigame;
        }
    }
}
