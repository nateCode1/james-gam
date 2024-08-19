using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{

    private bool shouldMove = false;
    private float timeElapsed = 0.0f;
    private bool canKickOut = false;

    public Bobber bobber;

    public void fishAscent() {
        shouldMove = true;
    }

    private void FixedUpdate(){
        if(shouldMove && (transform.position.y < -0.267)) {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if(shouldMove) {
            transform.position = new Vector3 ((float)transform.position.x,-0.267f,(float)transform.position.z);
            shouldMove = false;
            timeElapsed = 0.0f;
            canKickOut = true;
         }
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 5f && canKickOut) {
            SceneManager.UnloadSceneAsync("FishingMinigame");
        }
    }
}
