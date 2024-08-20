using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyPlatform : MonoBehaviour
{
    public GameObject lever;
    public GameObject platform;
    
    void Update()
    {
        if (lever.GetComponent<Lever>().flipped)
        {
            platform.SetActive(true);
        }
    }

}
