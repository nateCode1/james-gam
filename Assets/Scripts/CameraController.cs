using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public Vector3[] camCheckOffsets;
    public LayerMask cameraSolidLayers;
    private Transform cameraPivotTransform;
    private float oldX = 0;
    private float oldY = 0;
    private float camDist;
    private Vector3 camOffs;

    void Start() {
        cameraPivotTransform = GetComponent<Transform>();
        camDist = (transform.GetChild(0).position - transform.position).magnitude;
        camOffs = (transform.GetChild(0).position - transform.position).normalized;
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

        // ### CAMERA CLIPPING FIX START ###
        // Vector from player to desired camera position
        Vector3 camDir = Quaternion.Euler(cameraPivotTransform.eulerAngles) * -Vector3.forward;
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, camDir, out raycastHit, camDist, cameraSolidLayers);

        // Additional checks not implemented lmao
        // foreach (Vector3 offs in camCheckOffsets) {
        //     print(offs);
        // }

        Debug.DrawLine(transform.GetChild(0).position, transform.position);

        float currDist = camDist;
        if (raycastHit.collider) currDist = (raycastHit.point - cameraPivotTransform.position).magnitude * 0.7f;
        currDist = Mathf.Clamp(currDist, 0.5f, 5000);
        transform.GetChild(0).position = cameraPivotTransform.position + camDir * currDist + Vector3.up * 1;
        // ### CAMERA CLIPPING FIX END ###

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
