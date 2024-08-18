using UnityEngine;

public class RandomizeDirection : MonoBehaviour
{
    void Start()
    {
        RandomizeObjectDirection();
    }

    void RandomizeObjectDirection()
    {
        float randomXRotation = Random.Range(0f, 360f);
        float randomYRotation = Random.Range(0f, 360f);
        float randomZRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(randomXRotation, randomYRotation, randomZRotation);
    }

}
