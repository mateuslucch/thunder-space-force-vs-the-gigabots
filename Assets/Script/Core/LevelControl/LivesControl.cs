using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesControl : MonoBehaviour
{
    [SerializeField] int numberLives = 3;

    TextControl textControl;
    BallCount ballCounter;

    private void Awake()
    {
        textControl = FindObjectOfType<TextControl>();
        ballCounter = GetComponent<BallCount>();
    }

    private void Start()
    {
        textControl.ChangeLifeNumber(numberLives);
    }

    public void ChangeLife(int lifeFactor)
    {
        numberLives += lifeFactor;
        textControl.ChangeLifeNumber(numberLives);
        if (numberLives == 0)
        {
            GetComponent<Level>().LosePath();
        }
    }
}
