using UnityEngine;

public class LoseColider : MonoBehaviour
{
    Level levelControl;
    BallLauncher ballLauncher;

    private void Start()
    {
        levelControl = FindObjectOfType<Level>();
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (levelControl.NumberBalls() == 1)
            {
                ballLauncher.RestartBallToPaddle(collision); //só posiciona bola se tiver uma unica
            }
            else
            {
                Destroy(collision.gameObject);
            }
            levelControl.ExtraBallsMethods();
        }
        else
        {
            Destroy(collision.gameObject); //destroi qualquer objeto que colide
        }
    }
}
