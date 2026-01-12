using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject ball;

    void Start()
    {

    }

    private void OnCameraBack()
    {
        transform.Translate(Vector3.forward * -500f * Time.deltaTime);
    }
    private void OnCameraForward()
    {
        transform.Translate(Vector3.forward * 500f * Time.deltaTime);
    }

    void Update()
    {
        if (ball != null)
        {
            transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, transform.position.z);
        }
        else
        {
            GameObject[]
            balls = GameObject.FindGameObjectsWithTag("Ball");
            if (balls.Length > 0)
            {
                ball = balls[0];
            }
        }
    }
}

