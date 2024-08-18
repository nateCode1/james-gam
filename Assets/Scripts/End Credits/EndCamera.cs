using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    public Camera orthoCamera;

    Vector3 velocity = new Vector3(0.0005f,-0.00005f,0);

    private void Awake() {
    }

    private void Start()
    {  
    }

    private void FixedUpdate() {
        if(transform.position.x < 0) {
            transform.position += velocity;
            if(orthoCamera.orthographicSize < 0.1) {
                orthoCamera.orthographicSize += 0.005f * Time.deltaTime; 
            }
            else {
                orthoCamera.orthographicSize += 0.01f * Time.deltaTime;
            }
        }
        else if(orthoCamera.orthographicSize < 0.5) {
            orthoCamera.orthographicSize += 0.02f * Time.deltaTime;
        }

    }

}
