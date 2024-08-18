using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public Fish fish;

    void start() {
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Bobber bobber = collision.gameObject.GetComponent<Bobber>();
        if (bobber != null) {
            fish.fishAscent();
        }
    }
}
