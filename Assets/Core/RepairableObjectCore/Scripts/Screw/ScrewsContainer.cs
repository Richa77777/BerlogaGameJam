using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ScrewsContainer : MonoBehaviour
{
    [SerializeField] private List<Screw> _screwsInDevice = new List<Screw>();
    [SerializeField] private GameObject _screwableObject; // Тот объект, который будет открываться при отвинчивании всех винтов.
    [SerializeField] private float _screwableObjectMoveTime = 1f;
    [SerializeField] private float _screwableObjectMoveDistance = 1f;

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
            DOTween.Sequence()
                .Append(_screwableObject.transform.DOMoveY(_screwableObject.transform.position.y + _screwableObjectMoveDistance, _screwableObjectMoveTime))
                .AppendCallback(OffScrewableObject);
        }
    }

    private void OffScrewableObject()
    {
        _screwableObject.SetActive(false);
    }
}
