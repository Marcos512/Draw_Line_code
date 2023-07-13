using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMover : MonoBehaviour
{
    [SerializeField]
    private BezierSegment path;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed = 0;

    [SerializeField]
    private bool _pingPong;

    [SerializeField,Range(0,1)]
    private float time = 0;

    private int direction = 1;

    private void Update()
    {
        time += Time.deltaTime * speed * direction;
        time = Mathf.Clamp01(time);

        if (_pingPong)
            PingPongMove();
        else
            StartToEndMove();

        transform.position = Bezier.GetPoint(path, time);
        transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void PingPongMove()
    {
        if (time == 0 || time == 1)
            direction *= -1;
    }

    private void StartToEndMove()
    {
        if (time == 1)
            time = 0;
    }
}
