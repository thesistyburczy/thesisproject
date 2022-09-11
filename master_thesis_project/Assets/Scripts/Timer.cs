using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float startTime;
    private float startLapTime;
    private float finalTime;

    private List<float> times = new List<float>();

    #region Singleton-instance

    private static Timer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static Timer GetInstance()
    {
        return instance;
    }

    #endregion

    // used to measure one time needed for the whole playthrough
    public void StartTime()
    {
        startTime = Time.time;
    }

    public void StopTime()
    { 
        finalTime = Time.time - startTime;
    }

    public float GetFinalTime()
    {
        return finalTime;
    }

    // used to measure short interactions, in this case choosing an answer
    public void StartLapTime() 
    {
        startLapTime = Time.time;
    }

    public float GetLapTime()
    {
        return Time.time - startLapTime;
    }

    public float GetTime(int index)
    {
        return times[index];
    }

    public void SetTime(float newTime)
    {
        times.Add(newTime);
    }

    public void ClearList()
    {
        times.Clear();
    }
}
