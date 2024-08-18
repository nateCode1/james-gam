using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArmItem : ArmItem
{
    public Transform beamStart;
    public Transform beamEnd; 
    void Update()
    {
        // Beam
        GetComponent<LineRenderer>().SetPositions(new Vector3[2] {beamStart.position, beamEnd.position});
    }
}
