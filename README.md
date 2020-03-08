# Unity Pausable Game Template
Simple template for adding non timescale pausing to a game. Includes a PausableObject class that you simple derive anything that needs to be paused.

It includes a few basic types to be automatically paused and resumed based on what they are.
Currently the list includes:
* Rigidbody2D
* Rigidbody
* Animator
* NavMeshAgent

It will also handle making sure they're set to whatever their previous data was. You can find these types in PausableDataTypes.cs, or add new ones. I will slowly expand the built in lists over time, or feel free to submit a request to push in another.

## Pausing
To actually pause, you'll use the EventManager.TriggerPause() or EventManager.TriggerResume() respectively. The PausableObject should handle the rest. 

You will also having to make sure you're overriding the specific functions so the PausableObject doesn't get skipped. Make sure Awake(), OnEnable(), OnDisable(), and OnDestroy() are marked to override, and you're calling their base class.

Additionally if you have code running in update or elsewhere, there is a simple paused bool. I normally just add something like this to the top of my update loop.
```cs
if(paused)
{
  return;
}
```

## Coroutine Pausing
So when I originally made this, I was using Coroutines and couldn't find an easy way to pause them.
What I've done is provide a modifier version of my simple [Unity Timer](https://github.com/7ark/UnityTimer). It derives from PausableObject and handles pausing with my system automatically. It works very similarly to Coroutines, you can see a variety of examples [here](https://github.com/7ark/UnityTimer/blob/master/Example.cs).
