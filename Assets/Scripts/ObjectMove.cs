using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for moving object between diffrent points.
/// </summary>
public class ObjectMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform[] points;

    private Vector3 currentTarget;
    private Transform currentPoint;
    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        currentPoint = points[currentIndex];
        transform.position = currentPoint.position;
    }

    private void Update()
    {
        MoveBetweeenPoints();
    }

    /// <summary>
    /// Moves object to next point
    /// </summary>
    private void MoveBetweeenPoints()
    {
        if (transform.position == currentPoint.position)
        {
            currentIndex++;
            if(currentIndex >= points.Length)
            {
                currentIndex = 0;
            }
            currentPoint = points[currentIndex]; 
            currentTarget = currentPoint.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
    }
}
