using UnityEngine;

/// <summary>
/// Should be attached to object with trigger collider.
/// It detects situation whenever player fells off to the abyss.
/// </summary>
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
