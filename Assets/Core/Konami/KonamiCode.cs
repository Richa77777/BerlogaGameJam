using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    [SerializeField] private List<ButtonDevice> _buttons;
    [SerializeField] private GameObject _effect;
    private List<string> _typeSequence = new List<string>();
    private List<string> _needSequence = new List<string>(){"up", "up", "down", "down", "left", "right", "left", "right", "B", "A"};

    private void Start()
    {
        foreach (var button in _buttons)
        {
            button.OnClick += ClickButton;
        }
    }

    private void ClickButton(string key)
    {
        _typeSequence.Add(key);
        if(_needSequence[_typeSequence.Count-1] != _typeSequence[_typeSequence.Count-1])
            _typeSequence.Clear();
        else if (_typeSequence.Count == _needSequence.Count)
        {
            CompleteCode();
        }
    }

    private void CompleteCode()
    {
        foreach (var button in _buttons)
        {
            button.OnClick -= ClickButton;
        }
        _effect.SetActive(true);
    }
    
}
