using UnityEngine;

public class LoseColider : MonoBehaviour
{

    LivesControl livesControl;
    BallCount ballCountController;
    BallLauncher ballLauncher;

    private void Awake()
    {
        ballCountController = FindObjectOfType<BallCount>();
        ballLauncher = FindObjectOfType<BallLauncher>();
        livesControl = FindObjectOfType<LivesControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (ballCountController.NumberBalls() == 1) //check number balls
            {
                ballLauncher.RestartBallToPaddle(collision); //só posiciona bola se tiver uma unica
                livesControl.ChangeLife(-1);
            }
            else
            {
                Destroy(collision.gameObject); // destroi qualquer bola extra                
                ballCountController.RemoveBall(); // subtrai da quantia
            }

        }
        else { Destroy(collision.gameObject); }
    }
}
