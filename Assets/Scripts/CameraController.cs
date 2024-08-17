using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1.0f;
    private Transform cameraPivotTransform;
    private float oldX = 0;
    private float oldY = 0;

    void Start() {
        cameraPivotTransform = GetComponent<Transform>();
    }
    void Update()
    {
        float newX = oldX;
        float newY = oldY;
        if (Input.GetKey(KeyCode.Mouse2)) {
            newX = oldX - Input.GetAxisRaw("Mouse Y") * sensitivity;
            newY = oldY + Input.GetAxisRaw("Mouse X") * sensitivity;
        }
        cameraPivotTransform.eulerAngles = new Vector3(newX, newY, 0);

        oldX = newX;
        oldY = newY;
    }
}
