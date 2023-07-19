using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(DetailsContainer), typeof(DeviceLaunchButton))]
public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Vector3 _startRotation;
    private Tween _returnToStartPosTween;

    private bool _rotationIsBlocked = false;

    public DetailsContainer DetailsContainer { get; private set; }
    public DeviceLaunchButton DeviceLaunchButton { get; private set; }

    private void Awake()
    {
        _startRotation = transform.eulerAngles;

        _rotationSpeed *= 100;

        DetailsContainer = GetComponent<DetailsContainer>();
        DeviceLaunchButton = GetComponent<DeviceLaunchButton>();
    }

    private void Start()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.SetCurrentRotateableObject(this);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (_rotationIsBlocked == false)
                {
                    float xRotation = Input.GetTouch(0).deltaPosition.x * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
                    float yRotation = Input.GetTouch(0).deltaPosition.y * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

                    transform.Rotate(Vector3.up, -xRotation, Space.World);
                    transform.Rotate(Vector3.right, yRotation, Space.World);
                }
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

    public void BlockRotation()
    {
        _rotationIsBlocked = true;
    }

    public void UnblockRotation()
    {
        _rotationIsBlocked = false;
    }
}
