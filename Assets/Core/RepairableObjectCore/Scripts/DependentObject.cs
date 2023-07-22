using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependentObject : MonoBehaviour
{
    [SerializeField] private GameObject _dependentObject;

    private void OnDisable()
    {
        _dependentObject.SetActive(false);
    }
}
