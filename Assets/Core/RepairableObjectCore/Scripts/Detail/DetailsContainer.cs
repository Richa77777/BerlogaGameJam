using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsContainer : MonoBehaviour
{
    [SerializeField] private List<Detail> _details = new List<Detail>();

    public IReadOnlyCollection<Detail> GetDetails => _details;
}
