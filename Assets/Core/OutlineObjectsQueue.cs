using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObjectsQueue : MonoBehaviour
{
    [SerializeField] private List<GameObject> _interactableObjectsQueue = new List<GameObject>();

    private void Start()
    {
        IOutlined outlinedObject = null;
        IOutlined prevOutlinedObject = _interactableObjectsQueue[0].GetComponent<IOutlined>();

        prevOutlinedObject.TurnOnOutline();

        for (int i = 1; i < _interactableObjectsQueue.Count; i++)
        {
            if (_interactableObjectsQueue[i].TryGetComponent(out outlinedObject))
            {
                prevOutlinedObject.OnInteractEnded += outlinedObject.TurnOnOutline;
                prevOutlinedObject = outlinedObject;
            }
        }
    }
}

