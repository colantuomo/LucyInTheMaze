using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    [SerializeField]
    private Text killsTXT;
    [SerializeField]
    private Text salvationsTXT;
    [SerializeField]
    private UIEventsSO uIEventsSO;

    private void OnEnable()
    {
        uIEventsSO.pointsStateChange.AddListener(OnPointsChange);
    }

    private void OnDisable()
    {
        uIEventsSO.pointsStateChange.RemoveListener(OnPointsChange);
    }

    private void OnPointsChange(Points points)
    {
        Debug.Log("Points changed! " + points.Kills + "/" + points.Salvations);
        killsTXT.text = "Kills: " + points.Kills.ToString();
        salvationsTXT.text = "Salvations: " + points.Salvations.ToString();
    }
}
