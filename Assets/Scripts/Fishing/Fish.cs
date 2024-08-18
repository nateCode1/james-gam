using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private float newY = 0f;


    public void fishAscent() {
        while(transform.position.y < -0.15) {
            newY = transform.position.y +0.001f;
            transform.position = new Vector2 (transform.position.x, newY);
        }
    }
}
