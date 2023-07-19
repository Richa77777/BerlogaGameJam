using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ScrewsContainer : MonoBehaviour
{
    [SerializeField] private List<Screw> _screwsInDevice = new List<Screw>();
    [SerializeField] private GameObject _unscrewableObject; // Тот объект, который будет двигаться вверх и исчезать при отвинчивании всех винтов.
    [SerializeField] private float _unscrewableObjectMoveDistance = 1f;
    [SerializeField] private float _unscrewableObjectMoveTime = 1f;

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
                .Append(_unscrewableObject.transform.DOMoveY(_unscrewableObject.transform.position.y + _unscrewableObjectMoveDistance, _unscrewableObjectMoveTime))
                .AppendCallback(OffScrewableObject);
        }
    }

    private void OffScrewableObject()
    {
        _unscrewableObject.SetActive(false);
    }
}
