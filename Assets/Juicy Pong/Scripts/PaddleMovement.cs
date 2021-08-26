using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleMovement : MonoBehaviour
{
    [Tooltip("Checks who the player 1 is")]
    public bool player1;

    [Tooltip("Speed of the paddle")]
    [Range(100f, 1000f)]
    public float speed;

    /// <summary>
    /// Gets a reference to the rigidbody
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// player movement in the y axis
    /// </summary>
    private float yCoordMovement;

    /// <summary>
    /// Used to reset game
    /// </summary>
    private Vector3 _startPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Makes sure gravity is disabled
        _rb.gravityScale = 0;

        _startPos = transform.position;
    }

    private void Update()
    {
        UserInput();
    }

    private void FixedUpdate()
    {
        // Prevents player from moving in the x axis
        _rb.velocity = new Vector2(_rb.velocity.x, yCoordMovement * speed * Time.deltaTime);
    }

    /// <summary>
    /// Checks whether it is player 1 or 2
    /// Moves the player via the corresponding input
    /// </summary>
    private void UserInput()
    {
        if (player1) yCoordMovement = Input.GetAxisRaw("Vertical_1");

        else yCoordMovement = Input.GetAxisRaw("Vertical_2");

    }

    public void Reset()
    {
        _rb.velocity = Vector2.zero;
        transform.position = _startPos;
    }
}
