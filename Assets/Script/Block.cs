using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [Header("Configuration Parameters")]
    [SerializeField] AudioClip blockBreak;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    [SerializeField] GameObject powerUps;
    [SerializeField] bool releasePowerUp = false;

    float health = 200;

    //cached reference
    Level level;
    PowerupControl powerupControl;

    //state variables
    int timesHit;

    private void Start()
    {
        powerupControl = FindObjectOfType<PowerupControl>();
        if (powerupControl == null)
        {
            Debug.Log("forgot powerupControl on level");
        }
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() //conta os blocos no começo
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable" || tag == "WinBlock")
        {
            level.CountBlocks();
            if (tag == "WinBlock")
            {
                level.WinBlocks();
            }
        }
    }

    //TESTE SE BOLA ATINGE
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable" || tag == "WinBlock")
        {
            HandleHit();
        }
    }

    //TESTE SE LASER ATINGE
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; } //testa se objeto que atingiu não possui DamageDealer
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            HandleHit();
            health = 200;
        }
    }

    private void HandleHit()  //destroi o bloco ou mostra proxima sprite, QUANDO ATINGIDO POR LASER OU BOLA
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1; //numero de hits que o bloco leva antes de ser destruido, tamanho do array definido para o bloco mais um
        if (timesHit == maxHits)
        {
            CallPowerUp();
            DestroyBlockFromInside();
        }
        else { ShowNextHitSprite(); }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name); //indica se tem um sprite faltando em algum bloco
        }
    }

    //DESTRUIÇÃO DE BLOCOS
    public void DestroyBlockFromOutside() //chamado de outra classe, usada quando termina a fase e o bloco não é destruido
    {
        DestroyAndPlay();
    }

    private void DestroyBlockFromInside() //proprio bloco se destroy, quando atingido por algo
    {
        if (tag == "WinBlock")
        {
            level.WinBlocksDestroyed();
            FindObjectOfType<GameSession>().AddToScore();
        }
        else if (tag == "Breakable")
        {
            level.BlockDestroyed();
            FindObjectOfType<GameSession>().AddToScore();  //roda o grupo AddToScore() no arquivo GameSession, contar o score
        }
        //fazendo (gameObject, 1f) ele leva um tempo para sumir
        DestroyAndPlay();
    }

    private void DestroyAndPlay() //toca som, joga faiscas e destroy o bloco
    {
        AudioSource.PlayClipAtPoint(blockBreak,
                                Camera.main.transform.position,
                                PlayerPrefsController.GetSfxVolume()); //pode usar "new Vector3(8,6,-2)" coordenadas da camera 
        TriggerSparklesVFX();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f); //destroi o objeto, após 1 seg(?)
    }

    //CHAMA OS POWER UPS!!!
    private void CallPowerUp()
    {
        if (powerupControl.CountBlocksToPowerup(releasePowerUp))
        {
            GameObject power = Instantiate(
                       powerUps,
                       transform.position,
                       transform.rotation) as GameObject;
            power.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2f);
        }
    }

}
