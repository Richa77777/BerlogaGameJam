using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public ObjectRotation CurrentRotateableObject { get; private set; }

    public void SetCurrentRotateableObject(ObjectRotation rotateableObject)
    {
        CurrentRotateableObject = rotateableObject;
    }
}