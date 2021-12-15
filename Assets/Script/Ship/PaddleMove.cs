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
    [SerializeField] float totalExtraShipTime = 10f;

    [SerializeField] float mousePaddleOffset = 0f;
    float screenWidthInUnitsx;

    GameObject ballObject;
    bool shipPaused = false;
    bool extraShipActive;
    float extraShipTime;
    float minX;
    float maxX;

    Animator myAnimator;
    Vector2 shipPos; //usado quando mouse unico controle
    GameSession theGameSession;
    Ball theBall;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        screenWidthInUnitsx = mainCamera.pixelWidth * mainCamera.orthographicSize * 2 / mainCamera.pixelHeight;

        minX = minStandartX;
        maxX = maxStandartX;
        myAnimator = GetComponent<Animator>();
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (shipPaused == false) { MoveWithMouse(); }

        if (extraShipTime > 0) { extraShipTime -= Time.deltaTime; }
        else { ReturnExtraShip(); }
    }

    private void MoveWithMouse()
    {
        Vector2 shipPos = new Vector2(transform.position.x, transform.position.y); //"transform.position.y" mantém na posição y que está no unity
        shipPos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = shipPos;
    }

    private float GetXPos() { return Input.mousePosition.x / Screen.width * screenWidthInUnitsx - mousePaddleOffset; }

    private void MoveObject(float deltaX)
    {
        print(deltaX);
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); //"transform.position.y" mantém na posição y que está no unity
        paddlePos.x = Mathf.Clamp(deltaX, minX, maxX);
        transform.position = paddlePos;
    }

    // start extra ship
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

    public void ExtraShipIdle() { myAnimator.SetBool("SingleShip", false); }
    // end extra ships

    public void PauseShip(bool pause) { shipPaused = pause; }

}
