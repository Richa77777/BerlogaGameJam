using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsContainer : MonoBehaviour
{
    [SerializeField] private List<Detail> _details = new List<Detail>();
    [SerializeField] private float _colorizeDuration = 0.5f;

    private Coroutine _colorizeAllDetailsCor;

    private int GetNotBrokenDetailsCount()
    {
        int count = 0;

        for (int i = 0; i < _details.Count; i++)
        {
            if (_details[i].GetIsBroken == false)
            {
                count++;
            }
        }

        return count;
    }

    public void ColorizeAllDetails()
    {
        if (_colorizeAllDetailsCor == null)
        {
            _colorizeAllDetailsCor = StartCoroutine(ColorizeAllDetailsCor(_colorizeDuration));
        }
    }

    private IEnumerator ColorizeAllDetailsCor(float duration)
    {
        for (int i = 0; i < _details.Count; i++)
        {
            if (_details[i].ColorizePossible() == true)
            {
                _details[i].Colorize(duration);
                yield return new WaitForSeconds(duration);
            }

            yield return null;
        }

        _colorizeAllDetailsCor = null;
    }
}
