using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSystem : MonoBehaviour
{
    [Header("Drag Mechanics")]
    [SerializeField] private Vector2 _fingerPosition;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _forceDirection;
    [SerializeField] private float _maxStretch = 3f;
    [SerializeField] private float _minStretch = 1f;
    [SerializeField] private float _power = 10f;
    [SerializeField] private bool _touchDown;

    [Header("Related GameObjects")]
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private GameObject _ball;

    [Header("Related Components")]
    [SerializeField] private Rigidbody2D _ballrb;
    [SerializeField] private TrajectoryRender _trajectory;
    [SerializeField] private TrailRenderer _ballTrail;

    

    void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _startPoint = GameObject.FindGameObjectWithTag("Respawn");
        _startPosition = _startPoint.transform.position;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballrb = _ball.GetComponent<Rigidbody2D>();
        _ballTrail = _ball.GetComponent<TrailRenderer>();
        _touchDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fingerPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            if (_fingerPosition.y - _startPosition.y < 1 && (_fingerPosition - _startPosition).magnitude < _maxStretch) _touchDown = true;
        }

        if (_touchDown)
        {
            _fingerPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            if(_startPosition.y - _fingerPosition.y > 0)
            {
                if ((_startPosition - _fingerPosition).magnitude < _maxStretch)
                {
                    _ball.transform.position = _fingerPosition;
                }
                else
                {
                    _ball.transform.position = _startPosition + (_fingerPosition - _startPosition).normalized * _maxStretch;
                }
                _forceDirection = _startPosition - (Vector2)_ball.transform.position;
                _trajectory.DrawLine(_ball.transform.position ,_forceDirection*_power);
            }
                
            
        }

        if (Input.GetMouseButtonUp(0) && _touchDown)
        {
            _touchDown = false;
            _trajectory.HideLine();
            _ballTrail.enabled = true;
            if ((_startPosition - _fingerPosition).magnitude < _minStretch)
            {
                _ball.transform.position = _startPosition;
                return;
            }
            _ballrb.isKinematic = false;
            _ballrb.velocity = (_forceDirection*_power);
            this.enabled = false;
        }
    }
}
