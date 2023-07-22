using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

[RequireComponent(typeof(Outline))]
public class UnpinDeviceObject : MonoBehaviour, IPointerClickHandler, IOutlined
{
    private Action _onUnpined;

    [SerializeField] private float _yMoveDistance = 5;
    [SerializeField] private float _moveTime = 2;

    [SerializeField] private bool _isScrewed = false;
    [SerializeField] private ScrewsContainer _screwsContainer; // if _isScrewed == true

    [SerializeField] private bool _colorizeDetailsAfterUnpin = false;

    private Outline _outline;

    Action IOutlined.OnInteractEnded { get => _onUnpined; set => _onUnpined = value; }

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 10f;
        _outline.OutlineColor = Color.blue;
        _outline.enabled = false;

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
        _outline.enabled = false;

        if (_isScrewed == false)
        {
            GameController.Instance.CurrentRotateableObject.BlockRotation(gameObject);

            DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.localPosition.y + _yMoveDistance, _moveTime))
                .AppendCallback(OffObject);
        }
    }

    private void OffObject()
    {
        _onUnpined?.Invoke();
        if (_colorizeDetailsAfterUnpin == true)
        {
            GameController.Instance.CurrentRotateableObject.DetailsContainerComponent.ColorizeAllDetails();
        }

        GameController.Instance.CurrentRotateableObject.UnblockRotation(gameObject);
        gameObject.SetActive(false);
    }

    private void Unscrew()
    {
        _isScrewed = false;
    }

    public void TurnOnOutline()
    {
        _outline.enabled = true;
    }
}
