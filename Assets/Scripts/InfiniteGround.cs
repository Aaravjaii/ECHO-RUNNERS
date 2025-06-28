using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public Transform player;          // Drag your Player here
    public float tileLength = 20f;    // Z-length of each plane
    public float recycleOffset = 40f; // How far behind before recycling

    void Update()
    {
        // If player has passed this tile far enough
        if (transform.position.z + tileLength < player.position.z - recycleOffset)
        {
            // Move this tile ahead of the farthest one
            transform.position += Vector3.forward * tileLength * 3;
        }
    }
}
