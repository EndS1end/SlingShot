using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberRenderer : MonoBehaviour
{
    [SerializeField] private LaunchSystem _launchSys;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private LineRenderer _rubber;
    [SerializeField] private GameObject _ball;
    [SerializeField] private Vector3 _ballOffset = new Vector3(0, 0.3f, 0);

    private void Start()
    {
        _rubber.positionCount = 2;
    }
    private void Update()
    {
        if((_ball.transform.position.y - _ballOffset.y < this.transform.position.y) && (_launchSys.enabled))
        {
            _rubber.SetPosition(0, this.transform.position);
            _rubber.SetPosition(1, _ball.transform.position - _ballOffset);
        }
        else
        {
            _rubber.SetPosition(0, this.transform.position);
            _rubber.SetPosition(1, _startPoint.transform.position);
        }
    }
}
