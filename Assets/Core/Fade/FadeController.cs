using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [SerializeField] private CanvasGroup _fadeCanvas;
    [SerializeField] private float _fadeDuration = 1f;

    private Coroutine _fadeCor;

    public float GetFadeDuration => _fadeDuration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _fadeCanvas.gameObject.SetActive(false);
    }

    public void Fade()
    {
        if (_fadeCor == null)
        {
            _fadeCor = StartCoroutine(FadeCor());
        }
    }

    private IEnumerator FadeCor()
    {
        _fadeCanvas.gameObject.SetActive(true);

        if (_fadeDuration > 0)
        {
            for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
            {
                _fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t / (_fadeDuration / 2));
                yield return null;
            }

            _fadeCanvas.alpha = 1;

            for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
            {
                _fadeCanvas.alpha = Mathf.Lerp(1f, 0f, t / (_fadeDuration / 2));
                yield return null;
            }

            _fadeCanvas.alpha = 0;
        }

        _fadeCanvas.gameObject.SetActive(false);
        _fadeCor = null;
    }
}
