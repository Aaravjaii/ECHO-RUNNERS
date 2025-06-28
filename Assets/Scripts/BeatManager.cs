using UnityEngine;
using System;  // Needed for Action delegate

public class BeatManager : MonoBehaviour
{
    public static event Action OnBeat;  // Global event for beats

    public float bpm = 75f;
    private float beatInterval;
    private float nextBeatTime;

    public AudioSource musicSource;
    public ScoreManager scoreManager;  // ✅ Added line

    void Start()
    {
        beatInterval = 60f / bpm;
        nextBeatTime = Time.time + beatInterval;
    }

    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            Debug.Log("BEAT at " + Time.time.ToString("F2"));
            nextBeatTime += beatInterval;

            OnBeat?.Invoke();  // Notify all listeners
            scoreManager.AddScore(10);  // ✅ Added line
        }
    }
}
