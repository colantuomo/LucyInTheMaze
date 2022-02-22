using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum States
{
    Idle,
    Fighting,
    Walking,
}

[CreateAssetMenu(fileName = "StateEventsOB", menuName = "ScriptableObjects/StateEventsOB")]
public class StateEventsOB : ScriptableObject
{
    [System.NonSerialized]
    public UnityEvent<States> stateChangeEvent;
    public States currentState;

    private void OnEnable()
    {
        currentState = States.Idle;
        if (stateChangeEvent == null)
        {
            stateChangeEvent = new UnityEvent<States>();
        }
    }

    public void OnStateChange(States states)
    {
        currentState = states;
        stateChangeEvent.Invoke(states);

    }
}
