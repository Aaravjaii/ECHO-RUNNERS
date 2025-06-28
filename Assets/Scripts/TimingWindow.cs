using UnityEngine;

public class TimingWindow : MonoBehaviour
{
    public float perfectWindow = 0.1f;
    public float goodWindow = 0.3f;

    private float lastBeatTime;
    private bool beatStarted = false;

    void OnEnable()
    {
        BeatManager.OnBeat += HandleBeat;
    }

    void OnDisable()
    {
        BeatManager.OnBeat -= HandleBeat;
    }

    private void HandleBeat()
    {
        lastBeatTime = Time.time;
        beatStarted = true;
    }

    public string GetRating()
    {
        if (!beatStarted)
        {
            return "Miss";  // Prevent early Perfect at start
        }

        float diff = Mathf.Abs(Time.time - lastBeatTime);

        if (diff <= perfectWindow)
            return "Perfect";
        else if (diff <= goodWindow)
            return "Good";
        else
            return "Miss";
    }
}
