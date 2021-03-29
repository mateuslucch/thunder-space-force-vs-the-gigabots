using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float minX = 3.4f;
    [SerializeField] float maxX = 12.6f;

    //[SerializeField] float mouseSensivity = 6f; //usar no desktop
    [SerializeField] float screenWidthInUnitsx = 16f; //usar para browser

    [SerializeField] GameObject ballObject;
    bool stretchCondition = false;
    Vector2 paddlePos; //usado quando mouse unico controle

    //float mouseSensivity = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab; //variavel gameobject, libera no unity pra linkar com o laser
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shotFrequency = 0.5f;
    float shotCounter;

    bool firePowerUp = false;
    bool paddlePaused = false;
    public GameObject extraBall;

    //chached references
    GameSession theGameSession;
    Ball theBall;



    // Use this for initialization
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
        theBall.StartBall();
    }

    // Update is called once per frame
    void Update()
    {
        //ORIGINAL!! NÃOO USAR EM DESKTOP
        //PARA POSIÇÃO DO MOUSE
        //Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnitsx);               //mostrar a posição do mouse no Debug, com .y só mostra de y
        //float mousePosInUnits1 = (Input.mousePosition.x / Screen.width * screenWidthInUnitsx); //OLD
        //float mousePosInUnits2 = (Input.mousePosition.y / Screen.width * screenWidthInUnitsy);

        if (paddlePaused == false)
        {
            //ORIGINAL. NÃO USAR NO DESKTOP
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); //"transform.position.y" mantém na posição y que está no unity
            paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
            transform.position = paddlePos;


            //do laser defender (NÃO USAR PARA BROWSER!!)
            /*
            var deltaX = Input.GetAxis("Horizontal") / Screen.width * mouseSensivity;
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);  //"Mathf.Clamp("posição do objeto", "limite minimo", "limite máximo")"
            transform.position = new Vector2(newXPos, transform.position.y);
           */

        }

        if (firePowerUp == true)
        {
            CountDownAndShoot();
        }

    }

    //do laser defender
    /*
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") / Screen.width * mouseSensivity;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX,minX, maxX);  //"Mathf.Clamp("posição do objeto", "limite minimo", "limite máximo")"
        transform.position = new Vector2(newXPos, transform.position.y);

        //Debug.Log  (newXPos);
    }
    //
    */

    public void PaddlePause()
    {
        paddlePaused = true;
    }

    public void PaddleUnPause()
    {
        paddlePaused = false;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            //USAR NO BROWSER
            return Input.mousePosition.x / Screen.width * screenWidthInUnitsx;
            //USAR NO DESKTOP
            //return Input.GetAxis("Horizontal");

        }
    }

    //esticar paddle(COLAR SPRITES MAIS ADIANTE)
    public void EnlargePaddle()
    {
        if (stretchCondition != true)
        {
            StartCoroutine(EnlargingPaddle());
            minX = 4.6f;
            maxX = 11.4f;
            transform.localScale += new Vector3(1.3f, 0, 0);
        }
    }
    IEnumerator EnlargingPaddle()
    {
        stretchCondition = true; //estica paddle
        yield return new WaitForSecondsRealtime(8); //conta tempo
        transform.localScale += new Vector3(-1.3f, 0, 0);
        stretchCondition = false;
        minX = 3.4f;
        maxX = 12.6f;
    }

    //lasers!!

    public void ActivateLasers()
    {
        firePowerUp = true;
        StartCoroutine(StopLasers());

    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = shotFrequency;
        }
    }

    private void Fire()
    {

        GameObject paddleLaser = Instantiate(
                   laserPrefab,
                   transform.position,
                   transform.rotation) as GameObject; //ver o que cada coisa faz
        paddleLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

    }
    IEnumerator StopLasers()
    {
        yield return new WaitForSecondsRealtime(5);
        firePowerUp = false;
    }
    //End Lasers!!

}
