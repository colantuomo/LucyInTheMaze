using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private Transform Player;

    [SerializeField]
    private FightStateSO fightStateSO;
    [SerializeField]
    private UIEventsSO uIEventsSO;
    private Transform currentEnemy;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        uIEventsSO.ResetPoints();
    }

    private void OnEnable()
    {
        fightStateSO.currentEnemyEvent.AddListener(OnEnemyChange);
    }

    private void OnDisable()
    {
        fightStateSO.currentEnemyEvent.RemoveListener(OnEnemyChange);
    }

    private void OnEnemyChange(Transform enemy)
    {
        currentEnemy = enemy;
    }

    public void Attack()
    {
        if (currentEnemy != null)
        {
            uIEventsSO.AddKill();
            fightStateSO.OnStateChange(FightStates.Kill);
        }
    }

    public void Absolve()
    {
        if (currentEnemy != null)
        {
            uIEventsSO.AddSaved();
            fightStateSO.OnStateChange(FightStates.Saved);
        }
    }
}
