using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        if(_ball.transform.position.y > 0)
        {
            transform.position = new Vector3(0, _ball.transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
