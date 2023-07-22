using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateCube : MonoBehaviour // скрипт для плавного вращения куба по оси Z
{
    public float rotationSpeed = 1f;
    public float rotationThreshold = 0.01f;

    private Quaternion targetRotation;
    public bool isRotating = false;

    void Start()
    {
        GetComponent<CorrectPositions>().RandomizePos();

        targetRotation = transform.rotation;

        GetComponent<CorrectPositions>().CheckStartPos();
    }

    public void RotateMethod()
    {
        targetRotation *= Quaternion.Euler(0f, 0f, 90f);
        isRotating = false;

        StartCoroutine(RotateCubeCor());
    }

    IEnumerator RotateCubeCor()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;

        GetComponent<CorrectPositions>().CheckPos();
    }
}
