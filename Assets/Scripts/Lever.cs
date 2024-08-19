using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public GameObject leverTop;
    public GameObject leverBall;
    public float rotateFrom;
    public float roatateTo;
    public float timeToSwitch;
    public float outlineSize = 15;
    [Header ("Action")]
    public Action onPress;
    public bool onlyRunOnce = true;

    public bool flipped = false;

    private float timer = 0;
    private bool lastFlipped = false;
    private bool activated = false;
    private Outline ol;
    private Vector3 initRot;

    void Start()
    {
        initRot = leverTop.transform.rotation.eulerAngles;
        leverTop.transform.eulerAngles = initRot + new Vector3(0, 0, rotateFrom);
        ol = leverBall.GetComponent<Outline>();
        ol.outlineWidth = outlineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (flipped != lastFlipped) {
            timer = flipped ? 0 : timeToSwitch;
            lastFlipped = flipped;
            if (flipped) {
                if (!activated || !onlyRunOnce) onPress();
                activated = true;
            }
        }

        if (flipped && timer < timeToSwitch) {
            leverTop.transform.eulerAngles = initRot + new Vector3(0, 0, rotateFrom + easeOutElastic(timer / timeToSwitch) * (roatateTo - rotateFrom));
            
            timer += Time.deltaTime;
        }
        else if (!flipped && timer > 0) {
            leverTop.transform.eulerAngles = initRot + new Vector3(0, 0, roatateTo - easeOutElastic(1 - (timer / timeToSwitch)) * (roatateTo - rotateFrom));
            
            timer -= Time.deltaTime;
        }
    }

    float easeOutElastic(float x) {
        const float c4 = (2 * Mathf.PI) / 3;

        return x <= 0
            ? 0
            : x >= 1
            ? 1
            : Mathf.Pow(2, -22 * x) * Mathf.Sin((float)(x * 10 - 0.75) * c4) + 1;
    }

    // Returns if lever is now in active state or inactive state
    public bool Flip() {
        flipped = !flipped;
        return flipped;
    }
}
