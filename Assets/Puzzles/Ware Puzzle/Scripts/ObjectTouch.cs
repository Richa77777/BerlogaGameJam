using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.touches[0].phase == TouchPhase.Began))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject; // Получаем объект, с которым произошло столкновение

                    if (!hitObject.GetComponent<RotateCube>().isRotating)
                        hitObject.GetComponent<RotateCube>().RotateMethod(); // Меняем переменную в объекте, с которым произошло столкновение
                }
            }
        }
    }
}
