using System.Collections;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline), typeof(Collider))]
public class Detail : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isBroken;
    [SerializeField] private SceneField _minigameScene;

    private Color _brokenColor;
    private Color _notBrokenColor;

    private Outline _outlineComponent;
    private Coroutine _colorizeCor;

    public bool GetIsBroken => _isBroken;

    private void Awake()
    {
        _brokenColor = Color.red;
        _notBrokenColor = Color.green;

        _outlineComponent = GetComponent<Outline>();

        Color color = _outlineComponent.OutlineColor;
        color.a = 0f;

        _outlineComponent.OutlineColor = color;
        _outlineComponent.OutlineWidth = 10;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isBroken == true)
        {
            if (_minigameScene.HasScene == true)
            {
                GameController.Instance.LoadMinigame(_minigameScene.Name);
                GameController.Instance.MinigameCompleted += RepairDetail;
                return;
            }

            Debug.LogError("Мини-игра не присвоена полю _minigameScene.");
        }
    }

    private void RepairDetail(string sceneName)
    {
        if (_isBroken == true && sceneName == _minigameScene.Name)
        {
            _isBroken = false;
            Colorize(0.5f);
            GameController.Instance.MinigameCompleted -= RepairDetail;
        }
    }

    public void Colorize(float duration)
    {
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
