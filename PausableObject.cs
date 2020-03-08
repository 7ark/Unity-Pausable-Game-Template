using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Handles enabling and disabling different Unity components.
/// Works directly with the EventManager
/// </summary>
public abstract class PausableObject : MonoBehaviour
{
    protected bool paused = false;

    private PausableData[] allPausableData;

    protected virtual void Awake()
    {
        allPausableData = allPausableData.Setup(gameObject);
    }

    protected virtual void OnEnable()
    {
        EventManager.Pause += OnPause;
        EventManager.Resume += OnResume;
    }

    protected virtual void OnDisable()
    {
        EventManager.Pause -= OnPause;
        EventManager.Resume -= OnResume;
    }

    protected virtual void OnPause()
    {
        paused = true;
        allPausableData.PauseAll();
    }
    protected virtual void OnResume()
    {
        paused = false;
        allPausableData.ResumeAll();
    }

    protected virtual void OnDestroy()
    {
        OnDisable();
    }
}