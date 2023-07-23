using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPositions : MonoBehaviour
{
    float[] anglesArray = { 0, 90, 180, -90 };

    public float[] correctRotation;
    public bool isCorrect = false;
    int possibleRotations = 1;

    WiresGameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<WiresGameManager>();

        Application.targetFrameRate = 60;
    }

    public void CheckStartPos()
    {
        if (possibleRotations == 2)
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

    public void RandomizePos()
    {
        possibleRotations = correctRotation.Length;

        int rand = Random.Range(0, anglesArray.Length);
        transform.eulerAngles = new Vector3(0, 0, anglesArray[rand]);
    }

    public void CheckPos()
    {
        if (possibleRotations == 2)
        {
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || Mathf.Floor(transform.eulerAngles.z) == correctRotation[1])
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
