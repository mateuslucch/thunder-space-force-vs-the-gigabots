using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip blockBreak;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject powerUps;
    [SerializeField] bool releasePowerUp = false;

    float health;
    [SerializeField] float blockMaxHealth = 200;


    Level level;
    PowerupControl powerupControl;
    NumberBlockControl blockControl;

    int timesHit;
    private void Awake()
    {
        level = FindObjectOfType<Level>();
        blockControl = FindObjectOfType<NumberBlockControl>();
    }

    private void Start()
    {
        health = blockMaxHealth;
        powerupControl = FindObjectOfType<PowerupControl>();
        if (powerupControl == null)
        {
            Debug.Log("forgot powerupControl on level");
        }
        CountBlocks();
    }

    private void CountBlocks() { blockControl.CountBlocks(tag); }

    // TEST IF BALL HIT (ONE HIT, ONE DAMAGE)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable" || tag == "WinBlock")
        {
            HandleHit();
        }
    }

    // TEST IF LASER HIT (DIFFERENT BEHAVIOUR FROM BALL)
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        LaserHit(damageDealer);
    }

    private void LaserHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            HandleHit();
            health = blockMaxHealth;
        }
    }

    private void HandleHit()  //destroi o bloco ou mostra proxima sprite, QUANDO ATINGIDO POR LASER OU BOLA
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1; //numero de hits que o bloco leva antes de ser destruido, tamanho do array definido para o bloco mais um
        if (timesHit == maxHits)
        {
            CallPowerUp();
            DestroyBlockPlaying();
        }
        else { ShowNextHitSprite(); }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) { GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex]; }
        else { Debug.LogError("Block sprite is missing from array " + gameObject.name); }
    }

    // BLOCK DESTRUCTION
    public void DestroyBlockWinLevel()
    {
        DestroyAndPlay();
    }

    private void DestroyBlockPlaying()
    {
        blockControl.DestroyBlockCount(tag);
        DestroyAndPlay();
    }

    private void DestroyAndPlay()
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
        Destroy(sparkles, 1f);
    }

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
