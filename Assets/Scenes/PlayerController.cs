using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float accelerationSpeed = 0;
    public float maxSpeed = 10;
    public float responsiveness = 1;
    public float friction = 0.5f;
    private Transform player;
    public Rigidbody wheel;

    void Start()
    {
        player = GetComponent<Transform>();
    }

    void Update()
    {
        // TODO: max speed 
        Vector3 currentVelocity = wheel.velocity;
        // Calculates player speed
        Vector3 playerInputForce = accelerationSpeed * (Input.GetAxisRaw("Forward") * player.forward + Input.GetAxisRaw("Sideways") * player.right).normalized;
        // Calculates friction
        Vector3 frictionForce = -currentVelocity * friction;
        // If nothing is being pressed, boost your friction to stop you faster
        if (playerInputForce.magnitude < 0.1) {
            frictionForce /= responsiveness;
        }
        // Adds the force
        wheel.AddForce(playerInputForce + frictionForce, ForceMode.Acceleration);

    }
}
