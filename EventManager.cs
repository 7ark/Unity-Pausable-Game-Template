using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action Pause;
    public static event Action Resume;

    public static void TriggerPause()
    {
        Pause?.Invoke();
    }

    public static void TriggerResume()
    {
        Resume?.Invoke();
    }
}
