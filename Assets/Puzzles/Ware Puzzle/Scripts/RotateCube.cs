using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float rotationSpeed = 1f; // �������� ��������
    public float rotationThreshold = 0.01f; // ��������� �������� ��� ���������� �������� ��������

    private Quaternion targetRotation; // ������� ��������
    public bool isRotating = false; // ����, �����������, ���� �� ��������

    float[] anglesArray = { 0, 90, 180, -90 };

    public float[] correctRotation;
    public bool isCorrect = false;
    int possibleRotations = 1;

    WiresGameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<WiresGameManager>();

        Application.targetFrameRate = 90;
    }
    void Start()
    {
        possibleRotations = correctRotation.Length;

        int rand = Random.Range(0, anglesArray.Length);
        transform.eulerAngles = new Vector3(0, 0, anglesArray[rand]);

        targetRotation = transform.rotation; // ���������� ������� �������� ����� �������� �������� ����

        if(possibleRotations == 2)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isCorrect = true;
                gameManager.correctRotation();
            }
        }
        if (possibleRotations == 1)
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isCorrect = true;
                gameManager.correctRotation();
            }
        }
    }

    public void RotateMethod()
    {
        // ������� ����� ������� �������� ����� ���������� 90 �������� � �������� �������� �� ��� Y
        targetRotation *= Quaternion.Euler(0f, 0f, 90f);
        isRotating = false; // ������������� ���� ��������

        // ��������� �������� ��� �������� ��������
        StartCoroutine(RotateCubeCor());
    }

    IEnumerator RotateCubeCor()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold)
        {
            // ������ ������� ��� � ����������� �������� ��������
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // ����������� ������ ������� �������� ��� ���������� �������� ��� �����������
        transform.rotation = targetRotation;
        isRotating = false; // ���������� ���� ��������

        if (possibleRotations == 2)
        {
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || Mathf.Floor(transform.eulerAngles.z) == correctRotation[1])
            {
                isCorrect = true;
                gameManager.correctRotation();
                Debug.Log($"{Mathf.Round(transform.eulerAngles.z)}");
            }
            else if (isCorrect)
            {
                isCorrect = false;
                gameManager.uncorrectRotation();
            }
        }
        if (possibleRotations == 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0])
            {
                isCorrect = true;
                gameManager.correctRotation();
            }
            else if (isCorrect)
            {
                isCorrect = false;
                gameManager.uncorrectRotation();
            }
        }
    }
}
