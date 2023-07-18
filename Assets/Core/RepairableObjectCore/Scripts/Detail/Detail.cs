using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Detail : MonoBehaviour
{
    [SerializeField] private bool _isBroken;
    [SerializeField] UnityEvent _startMinigameEvent;

    public bool GetIsBroken => _isBroken;
    public UnityEvent GetStartMinigameEvent => _startMinigameEvent;

    public void RepairDetail()
    {
        if (_isBroken == true)
        {
            _isBroken = false;
        }
    }
}
