using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresGameManager : MonoBehaviour // проверка на победу
{

    public GameObject wiresHolder;
    public GameObject[] wires;

    [SerializeField] int totalWires = 0;

    [SerializeField] GameObject WinConfetti;

    public int correctedWires = 0;

    void Start()
    {
        totalWires = wiresHolder.transform.childCount;

        wires = new GameObject[totalWires];

        for(int i = 0; i < wires.Length; i++)
        {
            wires[i] = wiresHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctRotation()
    {
        correctedWires++;

        if(correctedWires == totalWires) // Win
        {
            WinConfetti.SetActive(true);
        }
    }

    public void uncorrectRotation()
    {
        correctedWires--;
    }
}
