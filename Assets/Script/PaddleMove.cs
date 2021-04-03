using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float minX = 3.4f;
    [SerializeField] float maxX = 12.6f;
    [SerializeField] float extendedMaxBounds = 0f;
    [SerializeField] float extendedMinBounds = 0f;
    
    //[SerializeField] float mouseSensivity = 6f; //usar no desktop

    [SerializeField] float screenWidthInUnitsx = 16f; //usar para browser
    [SerializeField] GameObject ballObject;    
    bool paddlePaused = false;

    Animator myAnimator;

    Vector2 paddlePos; //usado quando mouse unico controle
    GameSession theGameSession;
    Ball theBall;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
        theBall.StartBall();
    }
    void Update()
    {
        //ORIGINAL!! NÃO USAR EM DESKTOP
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
    }

    //inicio enlarge paddle
    public void EnlargePaddle()
    {
        myAnimator.SetBool("Enlarge", true);
        StartCoroutine(EnlargingPaddle());
        minX = extendedMinBounds;
        maxX = extendedMaxBounds;
    }

    IEnumerator EnlargingPaddle()
    {
        yield return new WaitForSecondsRealtime(10); //conta tempo, antes de executar o proximo                                                   

        myAnimator.SetBool("Desenlarge", true);
        myAnimator.SetBool("Enlarge", false);
        minX = 3.4f;
        maxX = 12.6f;
    }

    public void EnlargeIdle() //chamado quando termina o desenlarge(EVENTO)
    {
        myAnimator.SetBool("Desenlarge", false);
    }
    //fim enlarge paddle

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
}
