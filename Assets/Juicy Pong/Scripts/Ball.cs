using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Tooltip("Speed of the paddle")]
    [Range(100f,1000f)]
    public float speed;

    /// <summary>
    /// Gets a reference to the rigidbody
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Used to reset game
    /// </summary>
    private Vector3 _startPos;

    public JuiceItUp juice;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Makes sure gravity is disabled
        _rb.gravityScale = 0;

        // Move ball in random position
        //Launch();
    }

    private void LateUpdate()
    {
        if (juice.ballRotation) juice.RotateBall();
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        _rb.velocity = new Vector2(speed * x * Time.deltaTime, speed * y * Time.deltaTime);
    }

    public void Reset()
    {
        _rb.velocity = Vector2.zero;
        transform.position = _startPos;
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (juice.ballFlash) StartCoroutine(juice.BallFlash());

        if (juice.ballScale) juice.BallHitAnimation();

        if (juice.ballWobble) juice.BallWobble();

        if (juice.cameraShake) juice.CameraShake();

    }
}
