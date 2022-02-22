using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float DEFAULT_ZOOM = 8f;
    public float zoomSpeed = 2f;
    public float fightZoom = 7f;

    public AnimationCurve curve;

    [SerializeField]
    private StateEventsOB stateEventsOB;

    private bool canZoomIn = false;


    private void Update()
    {
        if (canZoomIn)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, fightZoom, curve.Evaluate(zoomSpeed * Time.deltaTime));
        }
        else
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, DEFAULT_ZOOM, curve.Evaluate(zoomSpeed * Time.deltaTime));
        }
    }

    private void OnEnable()
    {
        stateEventsOB.stateChangeEvent.AddListener(OnStateChange);
    }

    private void OnDisable()
    {
        stateEventsOB.stateChangeEvent.RemoveListener(OnStateChange);
    }

    private void OnStateChange(States state)
    {
        if (state != States.Fighting)
        {
            ZoomOut();
            return;
        }
        ZoomIn();
    }

    private void ZoomIn()
    {
        canZoomIn = true;
    }

    private void ZoomOut()
    {
        canZoomIn = false;
    }
}
