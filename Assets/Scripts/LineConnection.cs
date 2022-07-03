using System;
using UnityEngine;

public class LineConnection : Singleton<LineConnection>
{
    public LineData[] lines;
    [SerializeField] private LineController lineController;
    private void Start()
    {
        foreach (var lineData in lines)
        {
            lineController.SetUpLine(lineData.points);
        }
    }
}

    
[Serializable]
public class LineData
{
    [SerializeField] public Transform [] points;
}
