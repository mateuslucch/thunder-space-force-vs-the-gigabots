using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    [Header("MoveWithControls")]
    [SerializeField] float minStandartX = 3.4f;
    [SerializeField] float maxStandartX = 12.6f;
    [SerializeField] float extendedMaxBounds = 0f;
    [SerializeField] float extendedMinBounds = 0f;
    [SerializeField] float paddleSpeed = 1f;

    float minX = 3.4f;
    float maxX = 12.6f;

    [SerializeField] float totalExtraShipTime = 10f;
    [SerializeField] GameObject ballObject;

    bool paddlePaused = false;
    bool extraShipActive;
    float extraShipTime;

    Animator myAnimator;
    Vector2 paddlePos; //usado quando mouse unico controle
    GameSession theGameSession;
    Ball theBall;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    void Update()
    {

        if (paddlePaused == false) //condição, trocada quando abre e fecha menu
        {
            MoveWithControls();
        }

        if (extraShipTime > 0)
        {
            extraShipTime -= Time.deltaTime;
        }
        else
        {
            ReturnExtraShip();
        }
    }

    private void MoveWithControls()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * paddleSpeed * Time.deltaTime;
        MoveObject(deltaX);
    }

    public void MoveWithTouch(int direction)
    {
        float deltaX;
        deltaX = Time.deltaTime * paddleSpeed * direction;
        MoveObject(deltaX);
    }

    private void MoveObject(float deltaX)
    {
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
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
            return Input.GetAxis("Horizontal");
        }
    }

    //inicio extra paddle
    public void ExtraShip()
    {
        if (extraShipActive == false)
        {
            extraShipActive = true;
            myAnimator.SetBool("ExtraShip", true);
            minX = extendedMinBounds;
            maxX = extendedMaxBounds;
        }
        extraShipTime = totalExtraShipTime;
    }

    private void ReturnExtraShip()
    {
        if (extraShipActive == true)
        {
            extraShipActive = false;
            myAnimator.SetBool("SingleShip", true);
            myAnimator.SetBool("ExtraShip", false);
            minX = minStandartX;
            maxX = maxStandartX;
        }
    }
    public void EnlargeIdle()
    {
        myAnimator.SetBool("SingleShip", false);
    }
    //fim extra ships

    public void PaddlePause()
    {
        paddlePaused = true;
    }

    public void PaddleUnPause()
    {
        paddlePaused = false;
    }

}
