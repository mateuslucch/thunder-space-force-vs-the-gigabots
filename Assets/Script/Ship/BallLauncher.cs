using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Vector3 startOffset = new Vector3(0, 0.1f, 0);
    [SerializeField] AudioClip ballLaunch;

    MagnetPower magnetPowerControl;
    GameObject ball;
    Vector2 paddleToBallVector;

    private void Start()
    {
        var offset = new Vector3(0, 0.1f, 0);
        ball = Instantiate(ballPrefab, transform.position + startOffset, transform.rotation) as GameObject;
        magnetPowerControl = FindObjectOfType<MagnetPower>();
    }
    private void Update()
    {
        if (ball != null)
        {
            LockBalltoPaddle();
            LaunchOnPc();
        }
    }

    private void LockBalltoPaddle()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y + startOffset.y);
        ball.transform.position = paddlePos + paddleToBallVector;
    }

    public void RestartBallToPaddle(Collision2D collision)
    {
        ball = collision.gameObject;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    public void LaunchFromTouch()
    {
        if (ball != null)
        {
            LaunchBall();
        }
    }

    private void LaunchOnPc()
    {
        //if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        AudioSource.PlayClipAtPoint(ballLaunch, Camera.main.transform.position, PlayerPrefsController.GetSfxVolume());
        ball.GetComponent<BallMove>().LaunchBall();
        ball = null;
    }

    private void OnCollisionEnter2D(Collision2D objectCollided)
    {
        if (objectCollided.gameObject.tag == "Ball" && magnetPowerControl.IsMagnetsOn() == true && ball == null)
        {
            RestartBallToPaddle(objectCollided);

        }
    }
}
