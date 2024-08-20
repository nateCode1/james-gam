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
            newX = ClampAngle(newX, -50, 35);
            newY = oldY + Input.GetAxisRaw("Mouse X") * sensitivity;
        }
        cameraPivotTransform.eulerAngles = new Vector3(newX, newY, 0);

        oldX = newX;
        oldY = newY;
    }

    public void SetSensitivity (float newSens) {
        sensitivity = newSens;
    }

    float ClampAngle(float angle, float min, float max) {
	    float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        return Mathf.Clamp(angle, min + floor, max + floor);
    }
}
