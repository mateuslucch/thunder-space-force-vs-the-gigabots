using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] AudioClip ballSounds;

    GameObject extraBall;

    public void ExtraBalls()
    {
        var offset = new Vector3(0, 0.1f, 0);
        extraBall = Instantiate(gameObject, transform.position + offset, transform.rotation);
        extraBall.GetComponent<BallMove>().LaunchBall();
    }

    public void DestroyBall() { Destroy(gameObject); }
    public void DestroyOnHit() { Destroy(gameObject); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(ballSounds, Camera.main.transform.position, PlayerPrefsController.GetSfxVolume());
    }
}
