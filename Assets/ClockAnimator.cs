using System;
using UnityEngine;

public class ClockAnimator : MonoBehaviour
{
    // Based on https://unity3d.com/learn/tutorials/topics/scripting/simple-clock?playlist=17117
    // .NEt 4.6 targeted by https://docs.unity3d.com/Manual/ScriptingRuntimeUpgrade.html

    private const float hoursToDegrees = 360f / 12f;
    private const float minutesToDegrees = 360f / 60f;
    private const float secondsToDegrees = 360f / 60f;

    public Transform hours;
    public Transform minutes;
    public Transform seconds;

    public bool analogue;
    private PusherManager _pusherManager;

    void Start()
    {
        if (_pusherManager == null)
        {
            _pusherManager = GetComponent<PusherManager>();
        }
    }

    private void Update()
    {
        var time = DateTime.Now;

        if (analogue)
        {
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            hours.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalHours * -hoursToDegrees);
            minutes.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalMinutes * -minutesToDegrees);
            seconds.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalSeconds * -secondsToDegrees);
        }
        else
        {
            hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -hoursToDegrees);
            minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -minutesToDegrees);
            seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -secondsToDegrees);
        }

        if (time.Second == 0 ||
            time.Second == 15 ||
            time.Second == 30 ||
            time.Second == 45)
        {
            _pusherManager.Message($"The time sponsored by Pusher is {time.ToLongTimeString()}");
        }
    }


}
