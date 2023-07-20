using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Udar.SceneManager;

public class TestEnd : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.Instance.MinigameCompleted?.Invoke(GameController.Instance.CurrentMinigameName);
    }
}
