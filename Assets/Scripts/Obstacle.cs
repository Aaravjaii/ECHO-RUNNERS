using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public SpeedManager speedManager;

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