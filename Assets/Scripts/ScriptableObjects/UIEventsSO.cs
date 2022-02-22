using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Points
{
    public int Kills { get; set; }
    public int Salvations { get; set; }
    public Points(int kills, int salvations)
    {
        Kills = kills;
        Salvations = salvations;
    }
}

[CreateAssetMenu(fileName = "UIEventsSO", menuName = "ScriptableObjects/UIEventsSO")]
public class UIEventsSO : ScriptableObject
{
    [System.NonSerialized]
    public UnityEvent<Points> pointsStateChange;
    private Points _points;
    public int kills = 0;
    public int salvations = 0;

    private void OnEnable()
    {
        _points = new Points(kills, salvations);
        if (pointsStateChange == null)
        {
            pointsStateChange = new UnityEvent<Points>();
        }
    }

    public void ResetPoints()
    {
        salvations = 0;
        kills = 0;
        _points = new Points(kills, salvations);
    }

    public void AddKill()
    {
        kills++;
        _points.Kills = kills;
        OnPointsChange(_points);
    }

    public void AddSaved()
    {
        salvations++;
        _points.Salvations = salvations;
        OnPointsChange(_points);
    }

    private void OnPointsChange(Points points)
    {
        pointsStateChange.Invoke(points);
    }
}
