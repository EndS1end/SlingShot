using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroy : MonoBehaviour
{
    [SerializeField] private GameMechanicsScript GMScript;

    private void Start()
    {
        GMScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMechanicsScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            GMScript.GotHit();
        }
    }
}
