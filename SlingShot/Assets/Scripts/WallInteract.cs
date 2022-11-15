using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteract : MonoBehaviour
{
    [SerializeField] private AudioSource _wallHit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _wallHit.Play();
    }
}
