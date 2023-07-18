using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Screw : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _unscrewingTime = 0.75f;
    [SerializeField] private float _moveAfterUnscrewingTime = 1f;
    [SerializeField] private float _moveAfterUnscrewingDistance = 1f;
    [SerializeField] private float _yMoveDistance = 0.25f;

    private Tween _tween;
    private Coroutine _screwRotationCor;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_tween == null)
        {
            if (_screwRotationCor == null)
            {
                _screwRotationCor = StartCoroutine(ScrewRotationCor());
            }

            _tween = DOTween.Sequence().Append(transform.DOMoveY(transform.position.y + _yMoveDistance, _unscrewingTime))
                .AppendCallback(StopRotation);
        }
    }

    private IEnumerator ScrewRotationCor()
    {
        while (true)
        {
            transform.Rotate(0, Vector3.right.x * _rotationSpeed, 0);
            yield return null;
        }
    }

    private void StopRotation()
    {
        if (_screwRotationCor != null)
        {
            StopCoroutine(_screwRotationCor);
        }

        DOTween.Sequence()
            .SetDelay(0.05f)
            .Append(transform.DOMove(new Vector3(transform.position.x, transform.position.y + _moveAfterUnscrewingDistance, 
            transform.position.z), _moveAfterUnscrewingTime))
            .AppendCallback(OffGameObject);
    }

    private void OffGameObject()
    {
        gameObject.SetActive(false);
    }
}
