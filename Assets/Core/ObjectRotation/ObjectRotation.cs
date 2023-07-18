using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Button _returnRotationButton;

    private Vector3 _startRotation;
    private Tween _returnToStartPosTween;

    private void Awake()
    {
        _startRotation = transform.eulerAngles;
        _returnRotationButton.onClick.AddListener(ReturnToStartRotation);

        _rotationSpeed *= 100;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float xRotation = Input.GetTouch(0).deltaPosition.x * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
                float yRotation = Input.GetTouch(0).deltaPosition.y * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

                transform.Rotate(Vector3.up, -xRotation, Space.World);
                transform.Rotate(Vector3.right, yRotation, Space.World);
            }
        }
    }

    public void ReturnToStartRotation()
    {
        if (_returnToStartPosTween == null)
        {
            _returnToStartPosTween = DOTween.Sequence().Append(transform.DORotate(_startRotation, 0.5f)).AppendCallback(SetTweenToNull);
        }
    }

    private void SetTweenToNull()
    {
        _returnToStartPosTween = null;
    }
}
