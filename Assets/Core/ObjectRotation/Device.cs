using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(DetailsContainer), typeof(DeviceLaunchButton), typeof(OutlineObjectsQueue))]
public class Device : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Vector3 _startRotation;
    private Tween _returnToStartPosTween;

    private bool _rotationIsBlocked = false;
    private GameObject _lastBlockingObject;

    public OutlineObjectsQueue DetailsOutlineController { get; private set; }
    public DetailsContainer DetailsContainerComponent { get; private set; }
    public DeviceLaunchButton LaunchButtonComponent { get; private set; }
    public bool InRotation { get; private set; } = false;

    private void Awake()
    {
        _startRotation = transform.eulerAngles;

        _rotationSpeed *= 100;

        DetailsOutlineController = GetComponent<OutlineObjectsQueue>();
        DetailsContainerComponent = GetComponent<DetailsContainer>();
        LaunchButtonComponent = GetComponent<DeviceLaunchButton>();
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
        RotateDevice();
    }

    private void RotateDevice()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (_rotationIsBlocked == false)
                {
                    InRotation = true;
                    float xRotation = Input.GetTouch(0).deltaPosition.x * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
                    float yRotation = Input.GetTouch(0).deltaPosition.y * _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

                    transform.Rotate(Vector3.up, -xRotation, Space.World);
                    transform.Rotate(Vector3.right, yRotation, Space.World);

                    return;
                }
            }
        }

        InRotation = false;
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

    public void BlockRotation(GameObject blocker)
    {
        _lastBlockingObject = blocker;
        _rotationIsBlocked = true;
    }

    public void UnblockRotation(GameObject unblocker)
    {
        if (_lastBlockingObject == unblocker)
        {
            _rotationIsBlocked = false;
        }
    }
}
