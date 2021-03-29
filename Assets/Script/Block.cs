using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //configuration parameters
    [SerializeField] AudioClip blockBreak;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites; //variavel tipo sprites(?)

    [SerializeField] GameObject powerUps;
    [SerializeField] bool releasePowerUp = false;

    float health = 200;
    //cached reference
    Level level;

    //state variables
    [SerializeField] int timesHit; //serialized for debug reasons

    private void Start()
    {
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks() ///conta os blocos no começo
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable" || tag == "WinBlock") // "||" = "or" e "&&" = "and"
        {
            level.CountBlocks();      //roda o grupo CountBlocks(), contida no script level
            if (tag == "WinBlock")
            {
                level.WinBlocks();
            }
        }

    }

    //PROCESSO DESTRUIR COM BOLA
    private void OnCollisionEnter2D(Collision2D collision) //antiga, que destruia bloco diretamente, passou para HandleHit
    {

        if (tag == "Breakable" || tag == "WinBlock") //a "tag" se escolhe e configura no inspector
        {
            HandleHit();
        }

    }

    //PROCESSO DESTRUIR COM LASER
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer); //mantém damageDealer, se não não é chamado no metodo
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

    private void HandleHit()  //destroi blocos ou mostra proxima sprite
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1; //numero de hits que o bloco leva antes de ser destruido, tamanho do array definido para o bloco mais um
        if (timesHit == maxHits)
        {
            CallPowerUp();
            DestroyBlock(); //destroi blocos, adiciona ponto no score (GameSession.cs) e desconta total blocos no Level.cs

        }
        else
        {
            ShowNextHitSprite(); //mostra próxima sprite
        }
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

    private void DestroyBlock()
    {

        //ULTRAPASSADO // AudioSource.PlayClipAtPoint(blockBreak, Camera.main.transform.position, soundLevel.SfxVolume()); //pode usar "new Vector3(8,6,-2)" coordenadas da camera 
        AudioSource.PlayClipAtPoint(blockBreak, Camera.main.transform.position, PlayerPrefsController.GetSfxVolume()); //pode usar "new Vector3(8,6,-2)" coordenadas da camera 

        TriggerSparklesVFX();
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
        Destroy(gameObject);           //fazendo (gameObject, 1f) ele leva um tempo para sumir
    }
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f); //destroi o objeto, após 1 seg(?)
    }

    //CHAMA OS POWER UPS!!!
    public void PowerUpTrue()
    {
        releasePowerUp = true;
    }
    public void PowerUpFalse()
    {
        releasePowerUp = false;
    }
    private void CallPowerUp()
    {
        if (releasePowerUp == true)
        {
            GameObject power = Instantiate(
                       powerUps,
                       transform.position,
                       transform.rotation) as GameObject; //ver o que cada coisa faz
            power.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2f);


        }

    }

}
