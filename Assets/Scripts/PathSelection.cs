using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSelection : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4f;
    public float walkDelay = .5f;
    public float rotationSpeed = 7f;
    public RectTransform defaultPanel;
    public RectTransform fightPanel;
    private List<Transform> pathSelection;
    private List<Transform> lastPathSelection;
    private bool canWalkTroughPath = false;

    [SerializeField]
    private StateEventsOB stateEventsOB;
    [SerializeField]
    private FightStateSO fightStateSO;
    private Vector3 positionToRotate = Vector3.zero;

    void Start()
    {
        pathSelection = new List<Transform>();
        lastPathSelection = new List<Transform>();
    }

    void Update()
    {
        GetClickPosition();
        StartWalkingPath();
        if (pathSelection.Count == 0)
        {
            stateEventsOB.OnStateChange(States.Idle);
            canWalkTroughPath = false;
        }
        if (stateEventsOB.currentState == States.Fighting)
        {
            fightPanel.gameObject.SetActive(true);
            defaultPanel.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        fightStateSO.stateChangeEvent.AddListener(OnFightStateChanges);
    }

    private void OnDisable()
    {
        fightStateSO.stateChangeEvent.RemoveListener(OnFightStateChanges);
    }

    private void OnFightStateChanges(FightStates states)
    {
        RestartGameplay();
    }

    private void RestartGameplay()
    {
        RestartWalkingCycle();
        fightPanel.gameObject.SetActive(false);
        defaultPanel.gameObject.SetActive(true);
    }

    void GetClickPosition()
    {
        if (stateEventsOB.currentState != States.Idle)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Path")))
            {
                if (pathSelection.Find(x => x.transform.name == hit.transform.name))
                {
                    Debug.Log("You already selected that path!");
                    return;
                }
                if (IsNextPathClose(hit.transform))
                {
                    pathSelection.Add(hit.transform);
                    hit.transform.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    bool IsNextPathClose(Transform selectedPath)
    {
        int MIN_ACCEPTED_DISTANCE = 3;
        if (pathSelection.Count < 1)
        {
            if (Vector3.Distance(player.position, selectedPath.position) > MIN_ACCEPTED_DISTANCE)
            {
                return false;
            }
            return true;
        }
        Transform lastPath = pathSelection[pathSelection.Count - 1];
        if (Vector3.Distance(lastPath.position, selectedPath.position) > MIN_ACCEPTED_DISTANCE)
        {
            return false;
        }
        return true;
    }

    public void StartWalkingPath()
    {
        if (positionToRotate != Vector3.zero)
        {
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(positionToRotate), Time.deltaTime * rotationSpeed);
        }

        if (canWalkTroughPath)
        {
            stateEventsOB.OnStateChange(States.Walking);
            bool isLastIndex = pathSelection.Count < 2;
            if (pathSelection.Count == 0)
            {
                canWalkTroughPath = false;
                return;
            }
            int nextPathIndex = 1;
            if (isLastIndex)
            {
                nextPathIndex = pathSelection.Count - 1;
            }
            Vector3 positionToGo = new Vector3(pathSelection[0].position.x, player.position.y, pathSelection[0].position.z);
            Vector3 rotateToGo = new Vector3(pathSelection[nextPathIndex].position.x, player.position.y, pathSelection[nextPathIndex].position.z);
            player.position = Vector3.MoveTowards(player.position, positionToGo, Time.deltaTime * moveSpeed);
            positionToRotate = new Vector3(rotateToGo.x, player.position.y, rotateToGo.z) - player.position;
            if (HasReachedNextPosition(positionToGo))
            {
                canWalkTroughPath = false;
                StartCoroutine(GoToNextPosition());
            }
        }
    }

    private bool HasReachedNextPosition(Vector3 nextPosition)
    {
        return Vector3.Distance(player.position, nextPosition) < 0.1f;
    }

    IEnumerator GoToNextPosition()
    {
        pathSelection[0].GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(walkDelay);
        if (stateEventsOB.currentState != States.Fighting)
        {
            RestartWalkingCycle();
        }
    }

    private void RestartWalkingCycle()
    {
        stateEventsOB.OnStateChange(States.Walking);
        lastPathSelection.Add(pathSelection[0]);
        pathSelection.RemoveAt(0);
        canWalkTroughPath = true;
    }

    public void StartPath()
    {
        canWalkTroughPath = true;
    }

    public void PrintLastSelectedPath()
    {
        lastPathSelection.ForEach((path) =>
        {
            path.GetComponent<Renderer>().material.color = Color.blue;
        });
    }

    public void ResetPath()
    {
        if (pathSelection.Count == 0)
        {
            lastPathSelection.ForEach((path) =>
       {
           path.GetComponent<Renderer>().material.color = Color.white;
       });
            return;
        }
        lastPathSelection.Clear();
        pathSelection.ForEach((path) =>
        {
            lastPathSelection.Add(path);
            path.GetComponent<Renderer>().material.color = Color.white;
        });
        pathSelection.Clear();
    }
}
