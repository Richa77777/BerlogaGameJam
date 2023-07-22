using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrewsContainer : MonoBehaviour, IOutlined
{
    public Action AllUnscrewed;

    [SerializeField] private List<Screw> _screwsInDevice = new List<Screw>();

    Action IOutlined.OnInteractEnded { get => AllUnscrewed; set => AllUnscrewed = value; }

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

    public void TurnOnOutline()
    {
        for (int i = 0; i < _screwsInDevice.Count; i++)
        {
            _screwsInDevice[i].TurnOnOutline();
        }
    }
}
