using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum FightStates
{
    Kill,
    Saved,
}

[CreateAssetMenu(fileName = "FightStateSO", menuName = "ScriptableObjects/FightStateSO")]
public class FightStateSO : ScriptableObject
{
    [System.NonSerialized]
    public UnityEvent<FightStates> stateChangeEvent;
    [System.NonSerialized]
    public UnityEvent<Transform> currentEnemyEvent;
    [System.NonSerialized]
    public UnityEvent enemyDiedEvent;

    private void OnEnable()
    {
        if (stateChangeEvent == null)
        {
            stateChangeEvent = new UnityEvent<FightStates>();
        }
        if (currentEnemyEvent == null)
        {
            currentEnemyEvent = new UnityEvent<Transform>();
        }
        if (enemyDiedEvent == null)
        {
            enemyDiedEvent = new UnityEvent();
        }
    }

    public void OnStateChange(FightStates states)
    {
        stateChangeEvent.Invoke(states);
    }

    public void OnEnemySelected(Transform enemy)
    {
        currentEnemyEvent.Invoke(enemy);
    }

    public void OnEnemyDied()
    {
        enemyDiedEvent.Invoke();
    }
}
