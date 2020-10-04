using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //TODO: attach end Collision
    [SerializeField] private Transform startPosition;

    [SerializeField] private GameObject failWinPopUp;
    [SerializeField] private Text youWinFailText;

    [SerializeField] private GameObject approvalPopUp;

    private PlayerMovement player;

    private void Start()
    {
        failWinPopUp.SetActive(false);
        approvalPopUp.SetActive(false);

        player = FindObjectOfType<PlayerMovement>();

        player.transform.position = startPosition.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            approvalPopUp.SetActive(true);
        }
    }

    public void SetFailPopUp()
    {
        failWinPopUp.SetActive(true);
        youWinFailText.text = "You lose!";
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        failWinPopUp.SetActive(true);
        youWinFailText.text = "You win!";
    }
}
