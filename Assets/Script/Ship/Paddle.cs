using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shotFrequency = 0.5f;
    [SerializeField] float timeShooting = 5f;
    [SerializeField] GameObject[] guns;

    float timeToStop;
    float shotCounter;

    void Update()
    {
        if (timeToStop > 0)
        {
            CountDownAndShoot();
            timeToStop -= Time.deltaTime;
        }
    }

    //lasers!!
    public void ActivateLasers() { timeToStop = timeShooting; }

    public void StopLasers() { timeToStop = 0f; }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            foreach (GameObject gun in guns)
            {
                Fire(gun.transform.position);
            }
            shotCounter = shotFrequency;
        }
    }

    private void Fire(Vector3 gunPosition)
    {
        GameObject paddleLaser = Instantiate(
                   laserPrefab,
                   gunPosition,
                   transform.rotation) as GameObject; //ver o que cada coisa faz
        paddleLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

}
