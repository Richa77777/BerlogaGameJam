using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresGameManager : MonoBehaviour
{

    public GameObject wiresHolder;
    public GameObject[] wires;

    [SerializeField] int totalWires = 0;

    public int correctedWires = 0;

    private void Awake()
    {
        Debug.Log($"Width = {Screen.width} ; Height = {Screen.height}");
    }

    // Start is called before the first frame update
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

        if(correctedWires == totalWires)
        {
            Debug.Log("Win!");
        }
    }

    public void uncorrectRotation()
    {
        correctedWires--;
    }
}
