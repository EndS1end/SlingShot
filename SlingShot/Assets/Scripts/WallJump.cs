using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour

{
    [SerializeField] private int _power = 10;
    [SerializeField] private AudioSource _JumpSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _JumpSound.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += -collision.contacts[0].normal * _power;
        }
    }
}
