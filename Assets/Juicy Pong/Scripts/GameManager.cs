using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player Paddle")]
    public GameObject[] playerPaddle;

    [Header("Player Goal")]
    public GameObject[] playerGoal;

    [Header("Score UI")]
    public TextMeshProUGUI[] playerText;

    private int[] _playerScore;

    private void Awake()
    {
        _playerScore = new int[] { 0, 0 };
    }

    public void Score(int player)
    {
        playerText[player].text = (++_playerScore[player]).ToString();
        Reset();
    }

    private void Reset()
    {
        ball.GetComponent<Ball>().Reset();
        foreach (var paddle in playerPaddle)
        {
            paddle.GetComponent<PaddleMovement>().Reset();
        }
    }
}
