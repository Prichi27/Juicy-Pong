using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool player1;

    private GameManager _manager;
    private void Awake()
    {
        _manager = GameObject.Find("Game Management").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Ball"))
        {
            if (player1) _manager.Score(0);
            else _manager.Score(1);
        }
    }
}
