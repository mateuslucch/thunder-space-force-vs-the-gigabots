using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour
{
    [SerializeField] int maxBlocksToPowerup = 3;
    [SerializeField] int minBlocksToPowerup = 4;
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
            blocksToPowerup = Random.Range(minBlocksToPowerup, maxBlocksToPowerup);
            releasePowerup = true;
        }
        else { releasePowerup = false; }
        return releasePowerup;
    }

}
