using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Door : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float rotationAngle = 90;
    [SerializeField] private float moveTime = 0.75f;
    [SerializeField] private float moveAfterDistance = 0f;
    [SerializeField] private float moveAfterTime = 0.5f;
    [SerializeField] private Vector3 moveDistance;
    
    private Tween _tween;
    private bool _rotationCor = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tween == null)
        {

            GameController.Instance.CurrentRotateableObject.BlockRotation(gameObject);

            if (_rotationCor == false)
            {
                _rotationCor = true;
                RotationCor();
            }
            _tween = DOTween.Sequence().Append(transform.DOLocalMove(transform.localPosition + moveDistance, moveTime))
                    .AppendCallback(StopRotation);
            
        }
    }

    private void RotationCor()
    {
        transform.DOLocalRotate(new Vector3(-90, 0, rotationAngle), 2f).OnComplete(EndRotation);
    }
    private void EndRotation()
    {
        _rotationCor = false;
    }
    private void StopRotation()
    {
        DOTween.Sequence()
            .SetDelay(0.05f)
            .Append(transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + moveAfterDistance, 
                transform.localPosition.z), moveAfterTime))
            .AppendCallback(OffGameObject);
        
        _tween.Kill();
        _tween = null;
    }

    private void OffGameObject()
    {
        GameController.Instance.CurrentRotateableObject.UnblockRotation(gameObject);
        rotationAngle = rotationAngle*-1 - 90; 
    }
}
