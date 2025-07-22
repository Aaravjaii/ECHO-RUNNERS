using UnityEngine;

public class SidePropSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs; // Assign multiple prefabs in Inspector
    public float spawnInterval = 10f;
    public float offsetX = 6f;
    public float zStart = 20f;
    public int repeatCount = 20;

    private int spawnIndex = 0;
    private Transform player;
    private float lastPlayerZ = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        SpawnProps();
    }

    void Update()
    {
        if (player && player.position.z > lastPlayerZ + spawnInterval)
        {
            lastPlayerZ = player.position.z;
            SpawnProps();
        }
    }

    void SpawnProps()
    {
        int lastIndexLeft = -1;
        int lastIndexRight = -1;

        for (int i = 0; i < repeatCount; i++)
        {
            float zPos = zStart + (spawnIndex + i) * spawnInterval;

            // Spawn left side
            int indexLeft;
            do
            {
                indexLeft = Random.Range(0, rockPrefabs.Length);
            } while (indexLeft == lastIndexLeft && rockPrefabs.Length > 1);
            lastIndexLeft = indexLeft;

            Instantiate(rockPrefabs[indexLeft], new Vector3(-offsetX, 0, zPos), Quaternion.identity);

            // Spawn right side
            int indexRight;
            do
            {
                indexRight = Random.Range(0, rockPrefabs.Length);
            } while (indexRight == lastIndexRight && rockPrefabs.Length > 1);
            lastIndexRight = indexRight;

            Instantiate(rockPrefabs[indexRight], new Vector3(offsetX, 0, zPos), Quaternion.identity);
        }

        spawnIndex += repeatCount;
    }
}
