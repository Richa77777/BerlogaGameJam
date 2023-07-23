using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Door : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _rotationAngle = 90;
    [SerializeField] private float _doorOpenTime = 0.75f;
    [SerializeField] private float _moveTime = 0.75f;
    [SerializeField] private float _moveAfterTime = 0.5f;

    private Tween _doorTween;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_doorTween == null)
        {
            GameController.Instance.CurrentRotateableObject.BlockRotation(gameObject);

            RotationCor();
        }
    }

    private void RotationCor()
    {
        _doorTween = DOTween.Sequence().
            Append(transform.DOLocalRotate(new Vector3(-90, 0, _rotationAngle), _doorOpenTime))
            .AppendCallback(EndRotation);
    }

    private void EndRotation()
    {
        _doorTween = null;
        GameController.Instance.CurrentRotateableObject.UnblockRotation(gameObject);
        _rotationAngle = _rotationAngle == -90 ? 0 : -90;
    }
}
