using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shotFrequency = 0.5f;
    float shotCounter;

    bool firePowerUp = false;
        
    void Update()
    {
        if (firePowerUp == true)
        {
            CountDownAndShoot();
        }

    }    

    //lasers!!

    public void ActivateLasers()
    {
        firePowerUp = true;
        StartCoroutine(StopLasers());

    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = shotFrequency;
        }
    }

    private void Fire()
    {
        GameObject paddleLaser = Instantiate(
                   laserPrefab,
                   transform.position,
                   transform.rotation) as GameObject; //ver o que cada coisa faz
        paddleLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

    }
    IEnumerator StopLasers()
    {
        yield return new WaitForSecondsRealtime(5);
        firePowerUp = false;
    }
    //End Lasers!!

}
