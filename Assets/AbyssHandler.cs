using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssHandler : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.SetFailPopUp();
    }
}
