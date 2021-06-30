using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour
{
    [SerializeField] int maxBlocksToPowerup = 3; //const
    [SerializeField] int minBlocksToPowerup = 4; //const
    [SerializeField] int blocksToPowerup;

    private void Start()
    {
        blocksToPowerup = Random.Range(minBlocksToPowerup, maxBlocksToPowerup);
    }

    public bool CountBlocksToPowerup(bool releasePowerup)
    {        
        blocksToPowerup--;
        if (blocksToPowerup == 0)
        {
            Debug.Log("powerup liberado");
            blocksToPowerup = Random.Range(minBlocksToPowerup, maxBlocksToPowerup);
            releasePowerup = true;
        }
        else { releasePowerup = false;}
        return releasePowerup;
    }

}
