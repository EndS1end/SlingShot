using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    [SerializeField] private int MagnetForce;
    [SerializeField] private Rigidbody2D _ballrb;

    private void Start()
    {
        _ballrb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Vector2 Magnetism = (transform.position - collision.transform.position)*MagnetForce;
            transform.up = -Magnetism;
            _ballrb.AddForce(Magnetism);
        }
    }
}
