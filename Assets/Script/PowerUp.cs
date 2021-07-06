using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] GameObject extraBalls;

    //variaveis de sprites
    [SerializeField] public Sprite[] powerUpSprites;
    int pUElement;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        //pUElement = Random.Range(0, powerUpSprites.Length); //nao usar!! sem probabilidades, só randomico

        float randomPercentage = Random.Range(0f, 101f);
        if (randomPercentage <= 50f)
        {
            pUElement = Random.Range(0, 3);
            spriteRenderer.sprite = powerUpSprites[pUElement];
        }
        if (randomPercentage >= 51f && randomPercentage <= 94f)
        {
            pUElement = 3;
            spriteRenderer.sprite = powerUpSprites[pUElement];
        }
        if (randomPercentage >= 95f)
        {
            pUElement = 4;
            spriteRenderer.sprite = powerUpSprites[pUElement];
        }

        //usar para testes multiplos
        //pUElement = Random.Range(0,2);  
        //usar para testes individuais
        //pUElement = 2;

        spriteRenderer.sprite = powerUpSprites[pUElement];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle" || collision.gameObject.tag == "ExtraPaddle")
        {
            Destroy(gameObject);

            if (pUElement == 0) //magnets OK!!!
            {
                FindObjectOfType<MagnetPower>().MagnetsOn();
            }
            if (pUElement == 1)  //chamar mais bolas...ok!!
            {
                FindObjectOfType<Ball>().ExtraBalls(); //instatiate
                FindObjectOfType<Level>().AddBall(); //increase count
            }
            if (pUElement == 2) //extra ships OK!
            {
                FindObjectOfType<PaddleMove>().ExtraShip();
            }
            if (pUElement == 3) //lasers ok!!
            {
                FindObjectOfType<Paddle>().ActivateLasers();
            }
            if (pUElement == 4) //vida extra OK!!
            {
                FindObjectOfType<Level>().ExtraLife();
            }         

        }

    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
