using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLaunchButton : MonoBehaviour
{
    [SerializeField] private DetailsContainer _detailsContainer;

    public void Launch(float duration)
    {
        _detailsContainer.ColorizeAllDetails(duration);
    }
}
