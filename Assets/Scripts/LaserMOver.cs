using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public SpeedManager speedManager;
    void Start()
    {
        transform.parent = null; // Detach from drone
        Destroy(gameObject, 5f); // Moved here to avoid calling every frame
    }

    void Update()
    {
        if (speedManager != null)
        {
            float speed = speedManager.currentSpeed;
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
    void Awake()
    {
        if (speedManager == null)
        {
            speedManager = FindObjectOfType<SpeedManager>();
        }
    }

}