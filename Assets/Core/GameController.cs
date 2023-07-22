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

    public Action<string> SceneMinigameCompleted; // string - Scene.name

    public OutlineObjectsQueue CurrentDetailsOutlineController { get; private set; }
    public Device CurrentRotateableObject { get; private set; }
    public string CurrentMinigameName { get; private set; } = string.Empty;

    public void SetCurrentRotateableObject(Device rotateableObject)
    {
        if (CurrentRotateableObject == null)
        {
            CurrentRotateableObject = rotateableObject;
            CurrentDetailsOutlineController = rotateableObject.DetailsOutlineController;
        }
    }    

    public void LoadMinigame(string minigameSceneName)
    {
        if (CurrentMinigameName == string.Empty)
        {
            SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);

            CurrentRotateableObject.BlockRotation(gameObject);
            CurrentMinigameName = minigameSceneName;

            SceneMinigameCompleted += UnloadMinigame;
        }
    }

    public void UnloadMinigame(string minigameSceneName)
    {
        if (CurrentMinigameName != string.Empty)
        {
            CurrentRotateableObject.UnblockRotation(gameObject);
            CurrentMinigameName = string.Empty;

            SceneManager.UnloadSceneAsync(minigameSceneName);

            SceneMinigameCompleted -= UnloadMinigame;
        }
    }
}
