using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config parameters
    public GameObject other2;
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [SerializeField] AudioClip ballSounds; //arquivo som bola
    [SerializeField] AudioClip ballLaunch; //arquivo som de lançamento
    [SerializeField] float randomFactorY; //para trocar a direção da bola quando atinge objetos
    [SerializeField] float randomFactorX;

    [SerializeField] bool magnetsPower = false;

    float myVelocity;
    float xVel = 5f;
    float yVel = 12f;

    //state
    Vector2 velocityTweak;
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    bool gameIsPaused = false;

    //cache component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        float myAudioLevel = PlayerPrefsController.GetSfxVolume();        
        myAudioSource = GetComponent<AudioSource>();
        myVelocity = Mathf.Sqrt((xVel * xVel) + (yVel * yVel));
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void StartBall() //sendo chamado no paddle.cs
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    public void GamePausedDontLaunch()
    {
        gameIsPaused = true;
    }
    public void GameNotPausedLaunch()
    {
        gameIsPaused = false;
    }

    private void LaunchOnMouseClick()
    {
        if (gameIsPaused == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasStarted = true;

                //Toca som lançamento
                AudioClip launch = ballLaunch;
                myAudioSource.PlayOneShot(launch, PlayerPrefsController.GetSfxVolume());  //myAudioSource.PlayOneShot(variavel,volume)

                //LANÇAMENTO
                //xPush = Random.Range(-1,1);   //RANDOM X LAUNCH
                //xPush = Random.Range(0, 0); //0,0 PARA TESTES
                GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            }
        }
    }

    public void ReleaseClone()
    {
        hasStarted = true;
    }

    public void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //angulo colisao corrigido, minha solução
        float speedY = myRigidBody2D.velocity.y;
        float speedX = myRigidBody2D.velocity.x;
        //Debug.Log("x= " + speedX);
        //Debug.Log("y= " + speedY);

        if (speedX == 0)
        {
            randomFactorX = 0.4f;
        }
        else
        {
            randomFactorX = 0f;
        }
        if (speedY >= -1.6f && speedY <= 1.6f)
        {
            if (speedY >= 0)
            {
                randomFactorY = 0.8f;
            }
            else
            {
                randomFactorY = -0.8f;
            }
        }
        else
        {
            randomFactorY = 0f;
        }

        Vector2 velocityTweak = new Vector2(Random.Range(-randomFactorX, randomFactorX), randomFactorY);

        //angulo colisao randomico, ja a solução da comunidade, com random em x e y
        /*Vector2 velocityTweak = new Vector2         //angulo colisao randomico, ja a solução da comunidade, com random em x e y
            (Random.Range(-randomFactorX,randomFactorX), //evitar que a bola fique kikando na horizontal
            Random.Range(-randomFactorY,randomFactorY));
        */

        //sem alteração no angulo quando colide USAR PARA TESTES
        /*Vector2 velocityTweak = new Vector2   
            (Random.Range(0, 0), 
            Random.Range(0, 0));
        */

        if (hasStarted)
        {
            hasStarted = true;

            //SOM BOLA BATIDAS
            //AudioSource.PlayClipAtPoint(ballSounds, Camera.main.transform.position, soundLevel.SfxVolume());//ULTRAPASSADO
            AudioSource.PlayClipAtPoint(ballSounds, Camera.main.transform.position, PlayerPrefsController.GetSfxVolume());

            myRigidBody2D.velocity += velocityTweak;
            myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * myVelocity;

        }

        //magnets!!
        if (collision.gameObject.tag == "Paddle")
        {
            if (magnetsPower == true)
            {
                RestartBallToPaddle();
            }
        }
        //end magnets

    }

    //mais bolas PowerUp!!
    public void ExtraBalls()
    {

        var offset = new Vector3(0, 0.1f, 0);
        Instantiate(gameObject, transform.position + offset, transform.rotation);
        FindObjectOfType<Ball>().ReleaseClone();
    }
    //fim extra balls

    //REPOSICIONA a bola só quando sobra uma

    public void RestartBallToPaddle()
    {
        hasStarted = false;
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    public void DestroyBall()
    {
        Destroy(gameObject);
    }
    public void DestroyOnHit()
    {
        Destroy(gameObject);
    }

    //magnets!!

    public void MagnetBall()
    {
        StartCoroutine(MagnetsPowerUp());
    }

    IEnumerator MagnetsPowerUp()
    {
        magnetsPower = true;
        yield return new WaitForSecondsRealtime(3);
        magnetsPower = false;
    }
    //end magnets!!
}
