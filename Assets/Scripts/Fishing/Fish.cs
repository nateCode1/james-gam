using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    private bool shouldMove = false;

    public Bobber bobber;

    public void fishAscent() {
        shouldMove = true;
    }

    private void FixedUpdate(){
        if(shouldMove && (transform.position.y < -0.267)) {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        // if((transform.position.y = -0.267) && shouldMove) {
        //     shouldMove = false;
        // }
    }
}
