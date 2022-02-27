using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float rayLenght = 2f;
    private Vector3 positionToRay;

    [SerializeField]
    private StateEventsOB stateEventsOB;
    [SerializeField]
    private FightStateSO fightStateSO;

    void Update()
    {
        CheckForObstacle();
    }

    void CheckForObstacle()
    {
        positionToRay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Physics.Raycast(positionToRay, transform.forward, out RaycastHit hit, rayLenght))
        {
            if (hit.collider.tag == "Enemy")
            {
                stateEventsOB.OnStateChange(States.Fighting);
                fightStateSO.OnEnemySelected(hit.transform);
            }
        }

    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawRay(positionToRay, transform.forward * rayLenght);
    }
}
