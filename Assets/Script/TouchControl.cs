using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    int direction = 0;
     
    string objectName;

    void Start()
    {
        objectName = gameObject.name;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersection(ray, 1500f);

            //move buttons
            if (hits.collider.name == "Left Button")
            {
                direction = -1;
                FindObjectOfType<PaddleMove>().MoveWithTouch(direction);
                //print($"Mouse is over {hit.collider.name}");
            }
            if (hits.collider.name == "Right Button")
            {
                direction = 1;
                //print($"Mouse is over {hit.collider.name}");
                FindObjectOfType<PaddleMove>().MoveWithTouch(direction);
            }

            //launch ball button
            if (hits.collider.name == "Launch Ball Left" || hits.collider.name == "Launch Ball Right")
            {
                var balls = GameObject.FindObjectsOfType<Ball>();
                foreach (var ball in balls)
                {
                    ball.LaunchFromTouch();
                }
            }

            //menu button
            if (hits.collider.name == "Menu") { FindObjectOfType<Level>().TurnMenuOn(); }
        }
    }
}