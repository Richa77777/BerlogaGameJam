using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UnpinDeviceObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _yMoveDistance = 5;
    [SerializeField] private float _moveTime = 2;

    [SerializeField] private bool _isScrewed = false;
    [SerializeField] private ScrewsContainer _screwsContainer; // if _isScrewed == true

    private void Start()
    {
        if (_isScrewed == true)
        {
            if (_screwsContainer == null)
            {
                Debug.LogError("Вы не указали ScrewsContainer.");
            }
            else if (_screwsContainer != null)
            {
                _screwsContainer.AllUnscrewed += Unscrew;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isScrewed == false)
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + _yMoveDistance, _moveTime))
                .AppendCallback(OffObject);
        }
    }

    private void OffObject()
    {
        gameObject.SetActive(false);
    }

    private void Unscrew()
    {
        _isScrewed = false;
    }
}
