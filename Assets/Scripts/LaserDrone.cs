using UnityEngine;

public class LaserDroneMovement : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude, 0);
    }
}
