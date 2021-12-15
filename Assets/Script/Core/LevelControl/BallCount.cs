using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCount : MonoBehaviour
{
    [SerializeField] int numberBalls = 1;

    public void AddBall() { numberBalls++; }

    public void RemoveBall() { numberBalls--; }

    public int NumberBalls() { return numberBalls; }
}
