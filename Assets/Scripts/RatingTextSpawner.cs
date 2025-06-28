using UnityEngine;
using TMPro;

public class RatingTextSpawner : MonoBehaviour
{
    public GameObject ratingTextPrefab;
    public Transform spawnPoint;

    public void SpawnRating(string rating)
    {
        GameObject textObj = Instantiate(ratingTextPrefab, spawnPoint.position, Quaternion.identity);
        textObj.GetComponent<TextMeshPro>().text = rating;
        Destroy(textObj, 1f);  // Destroy after 1 second
    }
}
