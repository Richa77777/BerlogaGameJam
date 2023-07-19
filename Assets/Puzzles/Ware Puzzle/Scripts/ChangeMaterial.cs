using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material[] material;
    private int x = 0;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
    }

    public void ChangeMaterialMethod()
    {
        if (x == 0)
        {
            rend.sharedMaterial = material[x];
            x++;
        }
        else
        {
            x--;
            rend.sharedMaterial = material[x];
        }
    }
}
