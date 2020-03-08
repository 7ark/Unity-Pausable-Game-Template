using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface PausableData
{
    void Setup(object data);
    void Pause();
    void Resume();
}

//Rigidbody2D
public class PauseableRigidbody2D : PausableData
{
    private Rigidbody2D pauseRigidbody2D = null;
    private RigidbodyConstraints2D rbConstraints2D;

    public void Setup(object data)
    {
        pauseRigidbody2D = data as Rigidbody2D;
    }

    public void Pause()
    {
        rbConstraints2D = pauseRigidbody2D.constraints;
        pauseRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Resume()
    {
        pauseRigidbody2D.constraints = rbConstraints2D;
    }
}

//Rigidbody
public class PauseableRigidbody : PausableData
{
    private Rigidbody pauseRigidbody = null;
    private RigidbodyConstraints rbConstraints;
    public void Setup(object data)
    {
        pauseRigidbody = data as Rigidbody;
    }

    public void Pause()
    {
        rbConstraints = pauseRigidbody.constraints;
        pauseRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Resume()
    {
        pauseRigidbody.constraints = rbConstraints;
    }
}

//Animator
public class PausableAnimator : PausableData
{
    private Animator pauseAnimator = null;
    private float previousSpeed = 1;
    public void Setup(object data)
    {
        pauseAnimator = data as Animator;
    }

    public void Pause()
    {
        previousSpeed = pauseAnimator.speed;
        pauseAnimator.speed = 0;
    }

    public void Resume()
    {
        pauseAnimator.speed = previousSpeed;
    }
}

//AI
public class PausableNavMeshAgent : PausableData
{
    private NavMeshAgent pauseAgent = null;
    private bool aiPreviouslyEnabled = false;
    public void Setup(object data)
    {
        pauseAgent = data as NavMeshAgent;
    }

    public void Pause()
    {
        aiPreviouslyEnabled = pauseAgent.enabled;
        pauseAgent.enabled = false;
    }

    public void Resume()
    {
        pauseAgent.enabled = aiPreviouslyEnabled;
    }
}

public static class PausableDataExtensionMethods
{
    public static PausableData[] Setup(this PausableData[] pausableArray, GameObject gameObject)
    {
        List<PausableData> result = new List<PausableData>();

        //Need to manually add each derived type as a new one is added. Unsure if there's a better automated way to do this.

        //Rigidbody2D
        Rigidbody2D[] rigidbody2Ds = gameObject.GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D data in rigidbody2Ds)
        {
            PauseableRigidbody2D pausableRigidbody2D = new PauseableRigidbody2D();
            pausableRigidbody2D.Setup(data);
            result.Add(pausableRigidbody2D);
        }

        //Rigidbody
        Rigidbody[] rigidbodys = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody data in rigidbodys)
        {
            PauseableRigidbody pausableRigidbody = new PauseableRigidbody();
            pausableRigidbody.Setup(data);
            result.Add(pausableRigidbody);
        }

        //Animator
        Animator[] animators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator data in animators)
        {
            PausableAnimator pausableAnimator = new PausableAnimator();
            pausableAnimator.Setup(data);
            result.Add(pausableAnimator);
        }

        //NavMeshAgent
        NavMeshAgent[] agents = gameObject.GetComponentsInChildren<NavMeshAgent>();
        foreach (NavMeshAgent data in agents)
        {
            PausableNavMeshAgent pausableNavMeshAgent = new PausableNavMeshAgent();
            pausableNavMeshAgent.Setup(data);
            result.Add(pausableNavMeshAgent);
        }

        return result.ToArray();
    }

    public static void PauseAll(this PausableData[] pausableArray)
    {
        for (int i = 0; i < pausableArray.Length; i++)
        {
            pausableArray[i].Pause();
        }
    }
    public static void ResumeAll(this PausableData[] pausableArray)
    {
        for (int i = 0; i < pausableArray.Length; i++)
        {
            pausableArray[i].Resume();
        }
    }
}