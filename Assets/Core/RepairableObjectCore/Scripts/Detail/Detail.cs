using System.Collections;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline), typeof(Collider))]
public class Detail : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isBroken = false;
    [SerializeField] private bool _colorizeAtStart = false;
    [SerializeField] private bool _minigameOnScene = false;
    [SerializeField] private SceneField _minigameScene; // if _minigameOnScene == true

    private IRepairable _repairableObject;

    private Color _brokenColor = Color.red;
    private Color _notBrokenColor = Color.green;

    private Outline _outlineComponent;
    private Coroutine _colorizeCor;

    private bool _isClicked = false;

    public bool GetIsBroken => _isBroken;

    private void Awake()
    {
        _outlineComponent = GetComponent<Outline>();

        Color color = _outlineComponent.OutlineColor;
        color.a = 0f;

        _outlineComponent.OutlineColor = color;
        _outlineComponent.OutlineWidth = 10;

        _outlineComponent.enabled = false;
    }

    private void Start()
    {
        if (_isBroken == true && _minigameOnScene == false && !gameObject.TryGetComponent(out _repairableObject))
        {
            Debug.LogError("Объект IRepairable отсутствует на детали.");
        }


        if (_colorizeAtStart == true)
        {
            StartCoroutine(WaitRotateableObjectColorize());
        }
    }

    private IEnumerator WaitRotateableObjectColorize()
    {
        while (GameController.Instance == null || GameController.Instance.CurrentRotateableObject == null)
        {
            yield return null;
        }

        Colorize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isBroken == true && _isClicked == false)
        {
            _isClicked = true;

            if (_minigameOnScene == true)
            {
                if (_minigameScene.HasScene == true)
                {
                    GameController.Instance.LoadMinigame(_minigameScene.Name);
                    GameController.Instance.OnSceneMinigameCompleted += RepairDetailSceneMinigame;
                    return;
                }

                Debug.LogError("Объект SceneField не присвоен полю _minigameScene.");
            }

            else if (_minigameOnScene == false)
            {
                _repairableObject.OnRepairEnded += RepairDetailNotSceneMinigame;
            }
        }
    }

    private void RepairDetailNotSceneMinigame()
    {
        if (_isBroken == true)
        {
            _isBroken = false;
            Colorize();
            _repairableObject.OnRepairEnded -= RepairDetailNotSceneMinigame;
        }
    }
    private void RepairDetailSceneMinigame(string sceneName)
    {
        if (_isBroken == true && sceneName == _minigameScene.Name)
        {
            _isBroken = false;
            Colorize();
            GameController.Instance.OnSceneMinigameCompleted -= RepairDetailSceneMinigame;
        }
    }

    public void Colorize(float duration = 0.5f)
    {
        GameController.Instance.CurrentRotateableObject.DetailsContainerComponent.CheckBrokenDetails();

        if (_colorizeCor == null)
        {
            Color targetColor = _isBroken ? _brokenColor : _notBrokenColor;

            _colorizeCor = StartCoroutine(ColorizeCor(targetColor, duration));
        }
    }

    public bool ColorizePossible()
    {
        Color targetColor = _isBroken ? _brokenColor : _notBrokenColor;

        if (_outlineComponent.OutlineColor == targetColor)
        {
            return false;
        }

        return true;
    }

    private IEnumerator ColorizeCor(Color endColor, float duration)
    {
        if (duration > 0)
        {
            _outlineComponent.enabled = true;

            Color startColor = _outlineComponent.OutlineColor;
            Color currentColor = startColor;

            if (_outlineComponent.OutlineColor.a == 0)
            {
                _outlineComponent.enabled = true;
                currentColor = endColor;
                currentColor.a = 0f;

                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    currentColor.a = Mathf.Lerp(0f, 1f, t / duration);
                    _outlineComponent.OutlineColor = currentColor;
                    yield return null;
                }

                currentColor.a = 1f;
                _outlineComponent.OutlineColor = currentColor;
            }

            else if (_outlineComponent.OutlineColor.a > 0)
            {
                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    currentColor = Color.Lerp(startColor, endColor, t / duration);
                    _outlineComponent.OutlineColor = currentColor;
                    yield return null;
                }

                _outlineComponent.OutlineColor = endColor;
            }
        }

        _colorizeCor = null;
    }
}
