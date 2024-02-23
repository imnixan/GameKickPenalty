using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int time;

    private void Awake()
    {
        time = PlayerPrefs.GetInt("time");
    }

    IEnumerator TimerCounter()
    {
        WaitForSeconds second = new WaitForSeconds(1);
        while (true)
        {
            yield return second;
            time++;
        }
    }

    public int GetTime()
    {
        return time;
    }

    public void StartTime()
    {
        StartCoroutine(TimerCounter());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }
}
