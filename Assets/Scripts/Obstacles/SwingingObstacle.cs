using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for swinging obstacle behaviour.
/// It works like pendulum.
///
/// How to use it:
/// Drag to the scene Swinging Obstacle prefab.
/// When it's done set variables for the script as you want.
/// </summary>
public class SwingingObstacle : Obstacle
{
    [SerializeField, Tooltip("maximum angle, pendulum can tilt")] private float maximumAngle = 60f;
    [SerializeField, Tooltip("Wobble velocity")] private float angularVelocity = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomStartingVelocityDirection();
    }

    private void Update()
    {
        Swing();
    }

    /// <summary>
    /// Moves pendulum in direction.
    /// </summary>
    private void Swing()
    {
        if (transform.eulerAngles.z >= maximumAngle && transform.eulerAngles.z < 180)
        {
            rb.angularVelocity = Vector3.back * angularVelocity;
        }
        else if (transform.eulerAngles.z <= 360 - maximumAngle && transform.eulerAngles.z > 180) 
        {
            rb.angularVelocity = Vector3.forward * angularVelocity;
        }

    }

    /// <summary>
    /// Sets random velocity direction on start.
    /// </summary>
    private void SetRandomStartingVelocityDirection()
    {
        int[] randomDirection = new int[] { -1, 1 };
        int randomStartDirection = randomDirection[Random.Range(0, randomDirection.Length)];

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, (maximumAngle+1) * randomStartDirection);
    }
}
