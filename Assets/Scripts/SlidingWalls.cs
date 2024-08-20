using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingWalls : MonoBehaviour
{
    public GameObject lever;
    
    void Update()
    {
        if (lever.GetComponent<Lever>().flipped)
        {
            move();
        }
    }

    void move()
    {
        gameObject.SetActive(false);
    }

}
