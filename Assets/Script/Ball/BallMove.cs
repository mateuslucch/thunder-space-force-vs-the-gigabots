using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{

    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [SerializeField] float randomFactorY; //para trocar a direção da bola quando atinge objetos
    [SerializeField] float randomFactorX;

    float myVelocity;
    [SerializeField] float xVel = 5f;
    [SerializeField] float yVel = 12f;

    Vector2 velocityTweak;

    Rigidbody2D myRigidBody2D;

    void Start()
    {
        float myAudioLevel = PlayerPrefsController.GetSfxVolume();
        myVelocity = Mathf.Sqrt((xVel * xVel) + (yVel * yVel));
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall()
    {
        //xPush = Random.Range(-1,1);   //RANDOM X LAUNCH
        xPush = Random.Range(0, 0); //0,0 for tests
        GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //angulo colisao corrigido, parte comunidade, parte minha solução
        float speedY = myRigidBody2D.velocity.y;
        float speedX = myRigidBody2D.velocity.x;

        if (speedX == 0) { randomFactorX = 0.4f; }
        else { randomFactorX = 0f; }

        if (speedY >= -1.6f && speedY <= 1.6f)
        {
            if (speedY >= 0) { randomFactorY = 0.8f; }
            else { randomFactorY = -0.8f; }
        }
        else { randomFactorY = 0f; }

        Vector2 velocityTweak = new Vector2(Random.Range(-randomFactorX, randomFactorX), randomFactorY);

        myRigidBody2D.velocity += velocityTweak;
        myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * myVelocity;
    }
}
