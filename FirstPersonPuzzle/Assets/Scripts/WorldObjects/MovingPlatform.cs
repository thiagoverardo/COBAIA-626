using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public ClockUse clock;
    public Vector3[] points;
    public int currentPoint = 0;
    private Vector3 currentTarget;
    public float tolerance;
    public float speed;
    public float delayTime;

    private float delayStart;
    public bool automatic;
    void Start()
    {
        if (points.Length > 0)
            currentTarget = points[0];
        tolerance = speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        if(!clock.timeFreeze)
        {
            if (transform.position != currentTarget)
            {
                MovePlatform();
            }
            else
            {
                UpdateTarget();
            }
        }
    }

    void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }
    void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    void NextPlatform()
    {
        currentPoint++;
        if (currentPoint >= points.Length)
            currentPoint = 0;
        currentTarget = points[currentPoint];
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

}
