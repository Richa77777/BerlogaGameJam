using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsContainer : MonoBehaviour
{
    [SerializeField] private List<Detail> _details = new List<Detail>();

    private Coroutine _colorizeAllDetailsCor;

    public int GetNotBrokenDetailsCount()
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

    public void ColorizeAllDetails(float duration)
    {
        if (_colorizeAllDetailsCor == null)
        {
            _colorizeAllDetailsCor = StartCoroutine(ColorizeAllDetailsCor(duration));
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
