using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float xRotation = Input.GetTouch(0).deltaPosition.x * _rotationSpeed * Mathf.Deg2Rad;
                float yRotation = Input.GetTouch(0).deltaPosition.y * _rotationSpeed * Mathf.Deg2Rad;

                transform.Rotate(Vector3.up, -xRotation, Space.World);
                transform.Rotate(Vector3.right, yRotation, Space.World);
            }
        }
    }
}
