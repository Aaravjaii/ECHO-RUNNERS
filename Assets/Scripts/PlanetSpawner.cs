using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject[] planetPrefabs;
    public Camera mainCamera;

    [Header("Spawn Settings")]
    public float distanceFromCamera = 150f;
    public float sideOffset = 25f;
    public float verticalOffsetMin = 20f;
    public float verticalOffsetMax = 40f;
    public float spawnInterval = 1f;

    [Header("Planet Size Range")]
    public float minPlanetScale = 0.3f;
    public float maxPlanetScale = 0.7f;

    [Header("Effects")]
    public float fadeInDuration = 1.5f;
    public float planetLifetime = 25f;
    public float rotationSpeed = 10f;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        StartCoroutine(SpawnPlanetsContinuously());
    }

    IEnumerator SpawnPlanetsContinuously()
    {
        while (true)
        {
            GameObject planet = SpawnPlanetInView();
            if (planet != null)
            {
                StartCoroutine(FadeInPlanet(planet));
                StartCoroutine(RotatePlanet(planet));
                Destroy(planet, planetLifetime);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject SpawnPlanetInView()
    {
        if (planetPrefabs.Length == 0 || mainCamera == null) return null;

        Vector3 spawnDirection = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        Vector3 up = mainCamera.transform.up;

        Vector3 spawnPos = mainCamera.transform.position + spawnDirection * distanceFromCamera;
        spawnPos += right * Random.Range(-sideOffset, sideOffset);
        spawnPos += up * Random.Range(verticalOffsetMin, verticalOffsetMax);

        GameObject prefab = planetPrefabs[Random.Range(0, planetPrefabs.Length)];
        GameObject planet = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Use customizable size
        float randomScale = Random.Range(minPlanetScale, maxPlanetScale);
        planet.transform.localScale = Vector3.one * randomScale;

        // Disable shadows
        foreach (var renderer in planet.GetComponentsInChildren<Renderer>())
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
        }

        return planet;
    }

    IEnumerator FadeInPlanet(GameObject planet)
    {
        Renderer[] renderers = planet.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = 0;
                    mat.color = c;

                    mat.SetFloat("_Mode", 2); // Transparent
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.renderQueue = 3000;
                }
            }
        }

        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeInDuration);

            foreach (Renderer r in renderers)
            {
                foreach (Material mat in r.materials)
                {
                    if (mat.HasProperty("_Color"))
                    {
                        Color c = mat.color;
                        c.a = alpha;
                        mat.color = c;
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator RotatePlanet(GameObject planet)
    {
        while (planet != null)
        {
            planet.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}
