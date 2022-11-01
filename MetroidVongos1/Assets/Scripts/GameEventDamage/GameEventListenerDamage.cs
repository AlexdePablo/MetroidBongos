// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine.Events;
using UnityEngine;

public class GameEventListenerDamage : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventDamage Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<float> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(float damage)
    {
        Response.Invoke(damage);
    }
}