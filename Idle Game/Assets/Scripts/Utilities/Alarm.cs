using UnityEngine;
using System.Collections;

public class Alarm
{
    private float timer;
    private float elapsedTime;

    public float Timer
    {
        get { return timer; }
        private set { timer = value; }
    }

    public float TimeToRingTheAlarm
    {
        get { return elapsedTime; }
        private set { elapsedTime = value; }
    }

    public Alarm(float timer, bool alarmisRinging = true)
    {
        this.timer = timer;
        this.elapsedTime = alarmisRinging ? this.timer : 0.0f;
    }

    public bool IsRingingUpdated()
    {
        this.Update();

        bool alarmIsRinging = this.IsRinging();

        if (alarmIsRinging)
            this.Reset();

        return alarmIsRinging;
    }

    public bool IsRinging()
    {
        return this.elapsedTime >= this.timer;
    }

    public void Update()
    {
        this.elapsedTime += Time.deltaTime;
    }

    public void Reset()
    {
        this.elapsedTime = 0.0f;
    }

    public float Ratio()
    {
        return this.elapsedTime / this.timer;
    }
}