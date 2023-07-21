using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battery : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float moveTime = 0.75f;
    [SerializeField] private Vector3 moveDistance;
    [SerializeField] private Transform viewPosition;
    [SerializeField] private Slider slider; 
    [SerializeField] private Color sliderColorStart; 
    [SerializeField] private Color sliderColorEnd; 
    
    private Tween _tween;
    private Tween _moveToCamera;
    private Tween _charge;
    private Tween _changeSlider;
    private Tween _changeMaterial;
    private Material _chargeMaterial;
    private Vector3 _startPos;
    private Quaternion _startRot;

    private bool _inMiniGame;
    private void Start()
    {
        _chargeMaterial = transform.GetComponent<MeshRenderer>().materials[3];
        _chargeMaterial.color = sliderColorStart;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tween != null) return;
        
        _tween = DOTween.Sequence().Append(transform.DOLocalMove(transform.localPosition + moveDistance, moveTime))
            .AppendCallback(MoveToCamera);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_inMiniGame || _changeSlider != null) return;
        var lastSliderValue = slider.value;
        _changeSlider = slider.DOValue(slider.minValue, (-slider.minValue + lastSliderValue)).OnComplete(CompleteMiniGame);
        _changeMaterial = _chargeMaterial.DOColor(sliderColorEnd, (-slider.minValue + lastSliderValue));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_inMiniGame || _changeSlider == null || _changeMaterial == null) return;
        _changeSlider.Kill();
        _changeSlider = null;
        _changeMaterial.Kill();
        _changeMaterial = null;
    }
    private void MoveToCamera()
    {
        if (_moveToCamera != null) return;
        
        GameController.Instance.CurrentRotateableObject.BlockRotation(gameObject);
        
        _startPos = transform.localPosition;
        _startRot = transform.localRotation;
        _moveToCamera = DOTween.Sequence()
            .Append(transform.DOMove(viewPosition.position, 0.5f))
            .Append(transform.DORotateQuaternion(viewPosition.rotation, 0.5f))
            .AppendCallback(StartMiniGame);
    }
    private void StartMiniGame()
    {
        _inMiniGame = true;
    }
    private void CompleteMiniGame()
    {
        if (_charge != null) return;
        _inMiniGame = false;
        _charge = DOTween.Sequence()
             .Append(transform.DOLocalMove(_startPos, 0.5f))
             .Append(transform.DOLocalRotateQuaternion(_startRot, 0.5f))
             .AppendCallback(DeleteTweens);
    }

    public void DeleteTweens()
    {
        //End mini game < ---------------------------------------------------------------------
        _tween.Kill();
        _tween = null;
        moveDistance *= -1;
        GameController.Instance.CurrentRotateableObject.UnblockRotation(gameObject);
    }
    
}
