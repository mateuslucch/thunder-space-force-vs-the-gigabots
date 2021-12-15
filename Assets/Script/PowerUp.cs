using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] GameObject extraBalls;

    //variaveis de sprites
    [SerializeField] public Sprite[] powerUpSprites;
    [SerializeField] AudioClip[] powerUpSFX;

    int puEffect;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        float randomPercentage = Random.Range(0f, 101f);
        if (randomPercentage <= 50f)
        {
            puEffect = Random.Range(0, 2);
            spriteRenderer.sprite = powerUpSprites[puEffect];
        }
        if (randomPercentage >= 51f && randomPercentage <= 94f)
        {
            puEffect = 3;
            spriteRenderer.sprite = powerUpSprites[puEffect];
        }
        if (randomPercentage >= 95f)
        {
            puEffect = 4;
            spriteRenderer.sprite = powerUpSprites[puEffect];
        }

        // use for multiple powerups tests
        //pUElement = Random.Range(0,2);  
        // use for particular powerup test
        //puEffect = 2;

        spriteRenderer.sprite = powerUpSprites[puEffect];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle" || collision.gameObject.tag == "ExtraPaddle")
        {
            AudioSource.PlayClipAtPoint(powerUpSFX[puEffect], Camera.main.transform.position, PlayerPrefsController.GetSfxVolume());
            Destroy(gameObject);

            if (puEffect == 0)
            {
                FindObjectOfType<MagnetPower>().MagnetsOn();
            }
            if (puEffect == 1)
            {
                FindObjectOfType<Ball>().ExtraBalls();
                FindObjectOfType<BallCount>().AddBall();
            }
            if (puEffect == 2)
            {
                FindObjectOfType<PaddleMove>().ExtraShip();
            }
            if (puEffect == 3)
            {
                FindObjectOfType<Paddle>().ActivateLasers();
            }
            if (puEffect == 4)
            {
                FindObjectOfType<LivesControl>().ChangeLife(+1);
            }
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
