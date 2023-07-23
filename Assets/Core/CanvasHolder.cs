using UnityEngine;

public class CanvasHolder : MonoBehaviour
{
    [SerializeField] private GameObject _canvasObject;

    private void Start()
    {
        GameController.Instance.OnMinigameStarted += OffCanvas;
        GameController.Instance.OnSceneMinigameCompleted += OnCanvas;
        GameController.Instance.OnRestart += RestartUnsubscribe;
    }

    private void OffCanvas()
    {
        _canvasObject.SetActive(false);
    }

    private void OnCanvas(string emptyString)
    {
        _canvasObject.SetActive(true);
    }

    private void RestartUnsubscribe()
    {
        GameController.Instance.OnMinigameStarted -= OffCanvas;
        GameController.Instance.OnSceneMinigameCompleted -= OnCanvas;
        GameController.Instance.OnRestart -= RestartUnsubscribe;
    }
}
