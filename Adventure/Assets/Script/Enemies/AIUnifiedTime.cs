using System.Collections;
using UnityEngine;

public abstract class AIUnifiedTime : MonoBehaviour
{
    [SerializeField] protected float secondsInterval = 0.5f;

    // Start both coroutines when enabled

    // Coroutine: wait for seconds, then sync with physics
    protected IEnumerator SecondsAndPhysicsRoutine()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            OnSecondsAndPhysicsTick();
        }
    }

    // Coroutine: wait for seconds only
    protected IEnumerator SecondsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsInterval);
            OnSecondsTick();
        }
    }

    // Virtual methods to override in subclasses
    protected virtual void OnSecondsAndPhysicsTick() { }
    protected virtual void OnSecondsTick() { }
}
