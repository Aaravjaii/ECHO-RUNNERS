using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform[] lanes;
    public Transform spawnPoint;

    private void OnEnable()
    {
        BeatManager.OnBeat += SpawnObstacle;
    }

    private void OnDisable()
    {
        BeatManager.OnBeat -= SpawnObstacle;
    }

    void SpawnObstacle()
    {
        int laneIndex = Random.Range(0, lanes.Length);
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

        GameObject prefab = obstaclePrefabs[prefabIndex];
        Vector3 spawnPos = lanes[laneIndex].position;
        spawnPos.z = spawnPoint.position.z;

        // ✅ Laser goes to 2.5 height (for crouching), others stay low
        if (prefab.name.Contains("Laser"))
        {
            spawnPos.y = 2.5f;
        }
        else
        {
            spawnPos.y = 0.5f;
        }

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
