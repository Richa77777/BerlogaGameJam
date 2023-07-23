using System;
using System.Collections;
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

    public Action<string> OnSceneMinigameCompleted; // string - Scene.name

    public Action OnMinigameStarted;
    public Action OnRestart;

    private Coroutine _loadUnloadCor;
    private Coroutine _restartGameCor;

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
            if (_loadUnloadCor == null)
            {
                _loadUnloadCor = StartCoroutine(LoadMinigameCor(minigameSceneName));
            }
        }
    }

    public void UnloadMinigame(string minigameSceneName)
    {
        if (CurrentMinigameName != string.Empty)
        {
            if (_loadUnloadCor == null)
            {
                _loadUnloadCor = StartCoroutine(UnloadMinigameCor(minigameSceneName));
            }
        }
    }

    private IEnumerator LoadMinigameCor(string minigameSceneName)
    {
        FadeController.Instance.Fade();

        CurrentRotateableObject.BlockRotation(gameObject);
        CurrentMinigameName = minigameSceneName;

        OnSceneMinigameCompleted += UnloadMinigame;

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        OnMinigameStarted?.Invoke();

        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        _loadUnloadCor = null;
    }

    private IEnumerator UnloadMinigameCor(string minigameSceneName)
    {
        FadeController.Instance.Fade();

        CurrentRotateableObject.UnblockRotation(gameObject);
        CurrentMinigameName = string.Empty;

        OnSceneMinigameCompleted -= UnloadMinigame;

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        SceneManager.UnloadSceneAsync(minigameSceneName);

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        _loadUnloadCor = null;
    }

    public void RestartGame()
    {
        if (_restartGameCor == null)
        {
            _restartGameCor = StartCoroutine(RestartGameCor());
        }
    }

    private IEnumerator RestartGameCor()
    {
        OnRestart?.Invoke();

        FadeController.Instance.Fade();

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(FadeController.Instance.GetFadeDuration / 2);

        _restartGameCor = null;
    }
}
