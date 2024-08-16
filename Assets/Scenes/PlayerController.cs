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
        Vector3 playerForce = accelerationSpeed * (Input.GetAxis("Forward") * player.forward + Input.GetAxis("Sideways") * player.right);
        // Calculates friction
        Vector3 frictionForce = (playerForce.normalized - currentVelocity.normalized) * friction;
        // Adds the force
        wheel.AddForce(playerForce + frictionForce, ForceMode.Acceleration);

    }
}
