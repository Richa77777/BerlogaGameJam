using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDevice : MonoBehaviour, IPointerClickHandler
{
    public string key;
    public Action<string> OnClick;
    private Sequence _clickButton;
    private float _startPosY;

    private void Start()
    {
        _startPosY = transform.localPosition.y;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_clickButton != null) return;

        _clickButton = DOTween.Sequence()
            .Append(transform.DOLocalMoveY(0f, 0.2f))
            .Append(transform.DOLocalMoveY(_startPosY, 0.2f))
            .OnComplete(KillTweens);
        OnClick?.Invoke(key);
    }

    private void KillTweens()
    {
        _clickButton.Kill();
        _clickButton = null;
    }
}
