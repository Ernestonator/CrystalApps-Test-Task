using UnityEngine;

/// <summary>
/// Script used for infinite object spinning.
/// </summary>
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
