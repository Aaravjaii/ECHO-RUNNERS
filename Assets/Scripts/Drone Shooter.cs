using UnityEngine;

public class DroneShooter : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform firePoint;
    public float delayBeforeFire = 1f;
    public float verticalOffset = 0.3f; // 🔹 Amount to raise the laser

    private bool hasFired = false;

    void Update()
    {
        if (!hasFired && Time.timeSinceLevelLoad >= delayBeforeFire)
        {
            if (laserPrefab != null && firePoint != null)
            {
                Vector3 spawnPos = firePoint.position + new Vector3(0, verticalOffset, 0); // 🔹 Raise laser slightly
                Instantiate(laserPrefab, spawnPos, firePoint.rotation);
                hasFired = true;
            }
        }
    }
}
