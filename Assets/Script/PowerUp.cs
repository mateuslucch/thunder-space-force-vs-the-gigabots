using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] GameObject extraBalls;

    //variaveis de sprites
    [SerializeField] public Sprite[] powerUpSprites;
    int pUElement;

    private SpriteRenderer spriteRenderer;    

    // Use this for initialization
    void Start ()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        //pUElement = Random.Range(0, powerUpSprites.Length); //nao usar!! sem probabilidades, só randomico
        
        //pUElement = 1;  //usar para testes individuais

        float randomPercentage = Random.Range (0f, 101f);
        if (randomPercentage <= 50f)
        {
          pUElement =   Random.Range(0, 3);
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
        
        spriteRenderer.sprite = powerUpSprites[pUElement];
        //Debug.Log(rand);
        //Debug.Log(pUElement);

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            Destroy(gameObject);

            if (pUElement == 0) //magnets OK!!!
            {
                FindObjectOfType<Ball>().MagnetBall();

            }
            if (pUElement == 1)  //chamar mais bolas...ok!!
            {
                FindObjectOfType<Ball>().ExtraBalls(); //adiciona uma bola "instantiate"
                FindObjectOfType<Level>().AddBall(); //adiciona +1 na conta de bolas

            }
            if (pUElement == 2) //stretch paddle OK!!
            {
                FindObjectOfType<PaddleMove>().EnlargePaddle();
            }
            if (pUElement == 3) //lasers  sem sons
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

    // Update is called once per frame
    void Update () {
		
	}
}
