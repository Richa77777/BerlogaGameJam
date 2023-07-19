using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UnpinDeviceObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _yMoveDistance;
    [SerializeField] private float _moveTime;

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOMoveY(transform.position.y + _yMoveDistance, _moveTime);
    }

    private void OffObject()
    {
        gameObject.SetActive(false);
    }
}
