using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingObstacle : Obstacle
{
    [SerializeField] private float maximumAngle = 60f;
    [SerializeField] private float angularVelocity = 10f;

    [SerializeField] private Rigidbody rb;

    private void Start()
    { 
        SetRandomStartingVelocityDirection();
    }

    private void Update()
    {
        Swing();
    }

    private void Swing()
    {
        if(transform.eulerAngles.z >= maximumAngle && transform.eulerAngles.z < 180)
        {
            rb.angularVelocity = Vector3.back * angularVelocity;
        }
        else if (transform.eulerAngles.z <= 360-maximumAngle && transform.eulerAngles.z > 180)
        {
            rb.angularVelocity = Vector3.forward * angularVelocity;
        }

    }

    private void SetRandomStartingVelocityDirection()
    {
        int[] randomDirection = new int[] { -1, 1 };
        int randomStartDirection = randomDirection[Random.Range(0, randomDirection.Length)];

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, (maximumAngle+1) * randomStartDirection);
    }
}
