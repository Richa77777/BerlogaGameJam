using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour // проверка на нажатие по объекту
{
    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MinigameCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.touches[0].phase == TouchPhase.Began))
        {
            Ray ray = _camera.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject; 

                    if (!hitObject.GetComponent<RotateCube>().isRotating)
                        hitObject.GetComponent<RotateCube>().RotateMethod();
                }
            }
        }
    }
}
