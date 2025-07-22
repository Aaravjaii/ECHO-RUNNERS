using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform[] lanes;
    public Transform spawnPoint;

    private int beatCounter = 0;
    private Dictionary<int, float> laneCooldowns = new Dictionary<int, float>();
    private float laneCooldownTime = 2f;

    private float difficultyTimer = 0f;
    private float difficultyInterval = 20f;
    private float minCooldown = 0.8f;

    private int extraObstacles = 0;
    private int maxObstacles = 2;

    private void OnEnable()
    {
        BeatManager.OnBeat += SpawnObstacle;
    }

    private void OnDisable()
    {
        BeatManager.OnBeat -= SpawnObstacle;
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyInterval)
        {
            difficultyTimer = 0f;
            laneCooldownTime = Mathf.Max(minCooldown, laneCooldownTime - 0.2f);
            if (extraObstacles < maxObstacles) extraObstacles++;
        }
    }

    void SpawnObstacle()
    {
        beatCounter++;
        if (beatCounter % 2 != 0) return;

        int spawns = 1 + extraObstacles;

        List<int> availableLanes = new List<int>();
        for (int i = 0; i < lanes.Length; i++)
        {
            if (!laneCooldowns.ContainsKey(i) || Time.time - laneCooldowns[i] >= laneCooldownTime)
                availableLanes.Add(i);
        }

        Shuffle(availableLanes);
        HashSet<string> usedObstacleNames = new HashSet<string>();

        int spawnCount = 0;
        foreach (int laneIndex in availableLanes)
        {
            if (spawnCount >= spawns) break;

            GameObject prefab = null;
            int attempts = 0;

            do
            {
                prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                attempts++;
            }
            while (usedObstacleNames.Contains(prefab.name) && attempts < 10);

            if (prefab == null) continue;
            usedObstacleNames.Add(prefab.name);

            Vector3 spawnPos = lanes[laneIndex].position;
            spawnPos.z = spawnPoint.position.z + 15f;

            if (prefab.name.Contains("Drone")) spawnPos.y = 2.2f;
            else if (prefab.name.Contains("Laser")) spawnPos.y = 2.0f;
            else spawnPos.y = 0.5f;

            GameObject obstacle = Instantiate(prefab, spawnPos, prefab.transform.rotation);
            StartCoroutine(FadeInAndOut(obstacle));

            laneCooldowns[laneIndex] = Time.time;
            spawnCount++;
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    IEnumerator FadeInAndOut(GameObject obj)
    {
        float fadeDuration = 1f;
        float visibleTime = 3f;
        List<Material> mats = new List<Material>();

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    SetMaterialFadeMode(mat);
                    Color col = mat.color;
                    col.a = 0f;
                    mat.color = col;
                    mats.Add(mat);
                }
            }
        }

        // Fade In
        float t = 0f;
        while (t < fadeDuration)
        {
            float alpha = t / fadeDuration;
            foreach (Material m in mats)
            {
                Color c = m.color;
                c.a = alpha;
                m.color = c;
            }
            t += Time.deltaTime;
            yield return null;
        }

        // Fully visible for some time
        foreach (Material m in mats)
        {
            Color c = m.color;
            c.a = 1f;
            m.color = c;
        }

        yield return new WaitForSeconds(visibleTime);

        // Fade Out
        t = 0f;
        while (t < fadeDuration)
        {
            float alpha = 1f - (t / fadeDuration);
            foreach (Material m in mats)
            {
                Color c = m.color;
                c.a = alpha;
                m.color = c;
            }
            t += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }

    void SetMaterialFadeMode(Material mat)
    {
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
