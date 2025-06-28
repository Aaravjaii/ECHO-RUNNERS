using UnityEngine;

public class SpawnPointFollower : MonoBehaviour
{
    public Transform player;        // Drag your player GameObject here
    public float forwardOffset = 20f; // Distance ahead of player

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.z = player.position.z + forwardOffset;
        transform.position = newPos;
    }
}
