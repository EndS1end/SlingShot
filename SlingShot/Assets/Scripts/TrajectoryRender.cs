using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRender : MonoBehaviour
{
    [SerializeField] private LineRenderer _TrajectoryLine;
    [SerializeField] private int _pointsCount;
    [SerializeField] private float _timeStep;
    private Vector3[] _points;

    private void Start()
    {
        _points = new Vector3[_pointsCount];
        _TrajectoryLine.positionCount = _pointsCount;
    }

    public void DrawLine(Vector3 BallPosition, Vector3 Velocity)
    {
        _TrajectoryLine.enabled = true;
        float CurTime;
        for(int i = 0; i < _pointsCount; i++)
        {
            CurTime = i * _timeStep;
            _points[i] = BallPosition + Velocity * CurTime + Physics.gravity * CurTime * CurTime / 2;
        }

        _TrajectoryLine.SetPositions(_points);
    }

    public void HideLine()
    {
        _TrajectoryLine.enabled = false;
    }
}
