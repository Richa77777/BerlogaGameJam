using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameController.Instance.RestartGame);
    }
}
