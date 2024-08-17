using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float pauseDuration = 1f;

    private Vector3 targetPosition;
    private bool isPaused;
    private Rigidbody rb;

    void Start()
    {
        targetPosition = pointA.position;
        isPaused = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isPaused)
        {
            float step = speed * Time.deltaTime;
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            rb.Move(Vector3.MoveTowards(transform.position, targetPosition, step), transform.rotation);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(PauseBeforeSwitching());
            }
        }
    }

    IEnumerator PauseBeforeSwitching()
    {
        isPaused = true;

        yield return new WaitForSeconds(pauseDuration);

        if (targetPosition == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }

        isPaused = false;
    }
}
