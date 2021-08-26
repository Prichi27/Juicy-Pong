using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceItUp : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    public GameObject ballModel;
    public GameObject upperWall;
    public GameObject lowerWall;
    public new Camera camera;

    [Header("Color")]
    public Color player1Color;
    public Color player2Color;
    public Color ballColor;
    public Color upperWallColor;
    public Color lowerWallColor;
    public Color cameraColor;
    public Color ballFlashColor;

    [Header("EaseIn Types")]
    public iTween.EaseType easeType;
    public iTween.EaseType ballScaleEaseType;

    [Header("Ball Scale")]
    public Vector3 ballScaleVector;

    [Header("Wall Stretch Scale")]
    public Vector3 wallScaleVectorDown;
    public Vector3 wallScaleVectorUp;

    [Header("Trigger")]
    public bool colorActive;
    public bool startAnimation;
    public bool ballFlash;
    public bool ballRotation;
    public bool ballScale;
    public bool ballWobble;
    public bool wallShake;
    public bool cameraShake;

    private Vector3 _playerStartPos;
    private bool _isFlashing;

    private void Start()
    {
        _playerStartPos = player1.transform.position;

        ChangeColor();

        if(startAnimation) StartCoroutine(StartAnimation());
        
        else ball.GetComponent<Ball>().Launch();
    }

    private void Update()
    {
    }
    public void ChangeColor()
    {
        if (colorActive)
        {
            player1.GetComponent<SpriteRenderer>().color = player1Color;
            player2.GetComponent<SpriteRenderer>().color = player2Color;
            ballModel.GetComponent<SpriteRenderer>().color = ballColor;
            upperWall.GetComponent<SpriteRenderer>().color = upperWallColor;
            lowerWall.GetComponent<SpriteRenderer>().color = lowerWallColor;
            camera.GetComponent<Camera>().backgroundColor = cameraColor;
        }

        else if (!colorActive)
        {
            player1.GetComponent<SpriteRenderer>().color = Color.white;
            player2.GetComponent<SpriteRenderer>().color = Color.white;
            ballModel.GetComponent<SpriteRenderer>().color = Color.white;
            upperWall.GetComponent<SpriteRenderer>().color = Color.white;
            lowerWall.GetComponent<SpriteRenderer>().color = Color.white;
            camera.GetComponent<Camera>().backgroundColor = Color.black;
        }
    }

    public IEnumerator StartAnimation()
    {
        iTween.MoveFrom(player1, iTween.Hash(
            "y", 10f,
            "time", 1,
            "easetype", easeType,
            "delay", UnityEngine.Random.Range(0,0.5f)
        ));

        iTween.MoveFrom(player2, iTween.Hash(
            "y", -10f,
            "time", 1,
            "easetype", easeType,
            "delay", UnityEngine.Random.Range(0,0.5f)
        ));

        iTween.ScaleFrom(ball, iTween.Hash(
            "scale", Vector3.zero,
            "time", 0.8f,
            "easetype", easeType,
            "delay", UnityEngine.Random.Range(0,0.5f)
        ));

        //while (Vector3.Distance(_playerStartPos, player1.transform.position) > 0.01f) yield return null;
        yield return new WaitForSeconds(1.5f);

        ball.GetComponent<Ball>().Launch();
    }

    public IEnumerator BallFlash()
    {
        ballModel.GetComponent<SpriteRenderer>().color = ballFlashColor;

        yield return new WaitForSeconds(0.2f);

        ballModel.GetComponent<SpriteRenderer>().color = ballColor;

    }

    public void RotateBall()
    {
        ballModel.transform.up = ball.GetComponent<Rigidbody2D>().velocity;
    }

    public void BallHitAnimation()
    {
        iTween.ScaleTo(ballModel, iTween.Hash(
            "scale", ballScaleVector,
            "time", 0.1f,
            "easetype", ballScaleEaseType
        ));

        iTween.ScaleTo(ballModel, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.1f
        ));
    }

    public void BallWobble()
    {
        iTween.ScaleTo(ballModel, iTween.Hash(
            "scale", ballScaleVector,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "looptype", iTween.LoopType.pingPong
        ));

        iTween.ScaleTo(ballModel, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.7f
        ));
    }

    public void WallShake(GameObject wall)
    {
        var originalScale = new Vector3(30, 1, 1);

        iTween.ScaleTo(wall, iTween.Hash(
            "scale", wallScaleVectorDown,
            "time", 0.1f,
            "easetype", ballScaleEaseType
        ));

        iTween.ScaleTo(wall, iTween.Hash(
            "scale", wallScaleVectorUp,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.1f
        ));

        iTween.ScaleTo(wall, iTween.Hash(
            "scale", wallScaleVectorDown,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.2f
        ));

        iTween.ScaleTo(wall, iTween.Hash(
            "scale", wallScaleVectorUp,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.3f
        ));

        iTween.ScaleTo(wall, iTween.Hash(
            "scale", originalScale,
            "time", 0.1f,
            "easetype", ballScaleEaseType,
            "delay", 0.4f
        ));
    }

    public void CameraShake ()
    {
        camera.GetComponent<Animator>().SetTrigger("Shake");
    }

}
