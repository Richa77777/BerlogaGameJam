using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Detail : MonoBehaviour
{
    [SerializeField] private bool _isBroken;
    [SerializeField] UnityEvent _startMinigameEvent;

    [SerializeField] private Color _brokenColor;
    [SerializeField] private Color _notBrokenColor;

    private Outline _outlineComponent;
    private Coroutine _colorizeCor;

    public bool GetIsBroken => _isBroken;
    public UnityEvent GetStartMinigameEvent => _startMinigameEvent;



    private void Awake()
    {
        _outlineComponent = GetComponent<Outline>();

        Color color = _outlineComponent.OutlineColor;
        color.a = 0f;

        _outlineComponent.OutlineColor = color;
        _outlineComponent.enabled = false;
    }

    public void RepairDetail()
    {
        if (_isBroken == true)
        {
            _isBroken = false;
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
        if (duration > 0 && _outlineComponent.OutlineColor != endColor)
        {
            Color startColor = _outlineComponent.OutlineColor;
            Color currentColor = startColor;

            if (_outlineComponent.enabled == false)
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

            else if (_outlineComponent.enabled == true)
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
