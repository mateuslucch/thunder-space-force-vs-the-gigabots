using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float minX = 3.4f;
    [SerializeField] float maxX = 12.6f;

    //[SerializeField] float mouseSensivity = 6f; //usar no desktop

    [SerializeField] float screenWidthInUnitsx = 16f; //usar para browser
    [SerializeField] GameObject ballObject;
    bool stretchCondition = false;
    bool paddlePaused = false;

    Vector2 paddlePos; //usado quando mouse unico controle
    GameSession theGameSession;
    Ball theBall;

    void Start()
    {
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

     //esticar paddle(usar sprites mais adiante)
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
