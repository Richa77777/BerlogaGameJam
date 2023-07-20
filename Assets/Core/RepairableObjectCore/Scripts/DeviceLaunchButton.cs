using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLaunchButton : MonoBehaviour
{
    private DetailsContainer _detailsContainer;

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

        _detailsContainer = GameController.Instance.CurrentRotateableObject.DetailsContainer;
    }

    public void Launch()
    {
        //if (_detailsContainer != null)
        //{
        //    _detailsContainer.ColorizeAllDetails();
        //}
    }
}
