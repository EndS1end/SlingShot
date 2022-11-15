using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private GameMechanicsScript _GMScript;

    private void Start()
    {
        _GMScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMechanicsScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            _GMScript.CoinCollected();
            Destroy(this.gameObject);
        }
    }
}
