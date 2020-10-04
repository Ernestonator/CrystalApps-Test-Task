using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    private Vector3 currentTarget;

    private void Start()
    {
        transform.position = firstPoint.position;
    }

    private void Update()
    {
        MoveBetweeenTwoPoints();
    }

    private void MoveBetweeenTwoPoints()
    {
        if (transform.position == firstPoint.position)
        {
            currentTarget = secondPoint.position;
        }
        else if(transform.position == secondPoint.position)
        {
            currentTarget = firstPoint.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
    }
}
