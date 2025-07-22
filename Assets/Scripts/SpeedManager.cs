using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float currentSpeed = 10f;
    public float acceleration = 0.5f;  // How fast speed increases per second
    public float maxSpeed = 50f;

    void Update()
    {
        currentSpeed += acceleration * Time.deltaTime;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;
    }
}