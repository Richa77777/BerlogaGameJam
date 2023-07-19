using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;


public class Screw : MonoBehaviour, IPointerClickHandler
{
    public Action<Screw> OnUnscrewed;

    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _unscrewingTime = 0.75f;
    [SerializeField] private float _moveAfterUnscrewingDistance = 0f;
    [SerializeField] private float _moveAfterUnscrewingTime = 0.5f;
    [SerializeField] private float _yMoveDistance = 0.25f;

    private Tween _tween;
    private Coroutine _screwRotationCor;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tween == null)
        {
            GameController.Instance.CurrentRotateableObject.BlockRotation(gameObject);

            if (_screwRotationCor == null)
            {
                _screwRotationCor = StartCoroutine(ScrewRotationCor());
            }

            _tween = DOTween.Sequence().Append(transform.DOLocalMoveY(transform.localPosition.y + _yMoveDistance, _unscrewingTime))
                .AppendCallback(StopRotation);
        }
    }

    private IEnumerator ScrewRotationCor()
    {
        while (true)
        {
            transform.Rotate(0, 0, Vector3.right.x * _rotationSpeed);
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
            .Append(transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + _moveAfterUnscrewingDistance, 
            transform.localPosition.z), _moveAfterUnscrewingTime))
            .AppendCallback(OffGameObject);
    }

    private void OffGameObject()
    {
        GameController.Instance.CurrentRotateableObject.UnblockRotation(gameObject);
        OnUnscrewed?.Invoke(this);
        gameObject.SetActive(false);
    }
}
