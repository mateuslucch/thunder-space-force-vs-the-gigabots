using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    [SerializeField] GameObject[] timelineObjects;

    [SerializeField] float[] delayBetweenAnimation;
    PlayableDirector animationTrack;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        for (int i = 0; i < timelineObjects.Length; i++)
        {
            StartCoroutine(PlayTimeline(i));
            if (i == timelineObjects.Length)
            {
                PlayAnimation();
            }
        }
    }
    IEnumerator PlayTimeline(int i)
    {
        yield return new WaitForSecondsRealtime(1f);
        animationTrack = timelineObjects[i].GetComponent<PlayableDirector>();
        animationTrack.Play();

    }
}
