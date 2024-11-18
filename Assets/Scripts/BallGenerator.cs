using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject ballPrefab;
    float span = 1.0f;
    float delta = 0;
    int radio = 1;
    float speed = -0.03f;

    public void SetParameter(float span, float speed, int radio)
    {
        this.span = span;
        this.speed = speed;
        this.radio = radio;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            //GameObject item;
        }
    }
}
