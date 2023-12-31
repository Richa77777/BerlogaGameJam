using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Udar.SceneManager;

public class TestEnd : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.Instance.OnSceneMinigameCompleted?.Invoke(GameController.Instance.CurrentMinigameName);
    }
}
