using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReturnRotationButtonSetEvent : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitGameController());
    }

    private IEnumerator WaitGameController()
    {
        while (GameController.Instance == null || GameController.Instance.CurrentRotateableObject == null)
        {
            yield return null;
        }

        GetComponent<Button>().onClick.AddListener(GameController.Instance.CurrentRotateableObject.ReturnToStartRotation);
    }
}
