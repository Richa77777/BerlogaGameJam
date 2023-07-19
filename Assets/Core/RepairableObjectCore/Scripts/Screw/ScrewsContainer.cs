using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrewsContainer : MonoBehaviour
{
    public Action AllUnscrewed;

    [SerializeField] private List<Screw> _screwsInDevice = new List<Screw>();


    private void Start()
    {
        for (int i = 0; i < _screwsInDevice.Count; i++)
        {
            _screwsInDevice[i].OnUnscrewed += RemoveScrew;
        }
    }

    private void RemoveScrew(Screw screw)
    {
        _screwsInDevice.Remove(screw);

        if (_screwsInDevice.Count <= 0)
        {
            AllUnscrewed?.Invoke();
        }
    }
}
