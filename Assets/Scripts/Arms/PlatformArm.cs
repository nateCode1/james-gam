using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlatformArm : InverseKinArmAtics
{
    public GameObject platformTemplate;
    private GameObject platform;
    
    public override void Pressed() {
        platform = Instantiate(platformTemplate, actualHandPos, Quaternion.identity);
    }
    public override void LetGo() {
        Destroy(platform);
    }
}
