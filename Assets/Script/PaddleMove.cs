using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    [Header("MoveWithControls")]
    [SerializeField] float minX = 3.4f;
    [SerializeField] float maxX = 12.6f;
    [SerializeField] float extendedMaxBounds = 0f;
    [SerializeField] float extendedMinBounds = 0f;    
    [SerializeField] float mouseSensivity = 6f; //usar no desktop
    [SerializeField] float paddleSpeed = 1f;

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

        if (paddlePaused == false) //condição, trocada quando abre e fecha menu
        {
            //ORIGINAL. NÃO USAR NO DESKTOP
            /*
            Debug.Log(GetXPos());
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); //"transform.position.y" mantém na posição y que está no unity
            paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
            transform.position = paddlePos;
            */

            //COMANDOS USAR COM TECLADO
            MoveWithControls();
        }
    }

    public void MoveWithTouch(int direction)
    {
        float deltaX;
        deltaX = Time.deltaTime * paddleSpeed * direction;
        MoveObject(deltaX);
    }

    private void MoveWithControls()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * paddleSpeed * Time.deltaTime;
        MoveObject(deltaX);
    }

    private void MoveObject(float deltaX)
    {
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);  //"Mathf.Clamp(posição do objeto + incremento, "limite minimo", "limite máximo")"
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled()) //testes. paddle segue a bola
        {
            return theBall.transform.position.x;
        }
        else
        {
            //quando trocar lembrar de trocar nas configs de input na unity
            //USAR NO BROWSER
            //return Input.mousePosition.x / Screen.width * screenWidthInUnitsx;
            //USAR NO DESKTOP
            return Input.GetAxis("Horizontal");
            //usar no android
            //????
        }
    }

    //inicio enlarge paddle
    public void ExtraPaddles()
    {
        myAnimator.SetBool("Enlarge", true);
        StartCoroutine(ExtraPaddleRoutine());
        minX = extendedMinBounds;
        maxX = extendedMaxBounds;
    }

    IEnumerator ExtraPaddleRoutine()
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

}
