using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] float magnetsTime = 10f;

    bool magnetsPower = false;
    float magnetCountdown = 0f;

    private void Update()
    {
        if (magnetCountdown > 0) { magnetCountdown -= Time.deltaTime; }
        else { magnetsPower = false; }
    }

    public void MagnetsOn()
    {
        magnetCountdown = magnetsTime;
        magnetsPower = true;
    }
    
    public bool IsMagnetsOn() { return magnetsPower; }
}
