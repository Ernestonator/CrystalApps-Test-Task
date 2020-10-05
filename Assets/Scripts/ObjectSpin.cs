using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10;

    private Vector3 nextRotation;

    private void Start()
    {
        nextRotation = Vector3.up * (rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        transform.Rotate(nextRotation);
    }
}
