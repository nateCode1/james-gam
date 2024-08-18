using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public GameObject fish;

    private void OnCollisionEnter2D(Collision2D collision) {
        Bobber bobber = collision.gameObject.GetComponent<Bobber>();

        if (bobber != null) {
            
        }
    }
}
