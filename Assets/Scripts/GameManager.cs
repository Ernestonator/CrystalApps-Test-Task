using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;

    [SerializeField] private GameObject failWinPopUp;
    [SerializeField] private Text youWinFailText;
    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject approvalPopUp;
    [SerializeField] private GameObject infoPopUp;

    [SerializeField] private LayerMask playerMask;

    private PlayerMovement player;

    private void Start()
    {
        failWinPopUp.SetActive(false);
        approvalPopUp.SetActive(false);
        OpenHidePopUp(infoPopUp, true);

        player = FindObjectOfType<PlayerMovement>();

        player.transform.position = startPosition.position;
        restartButton.onClick.AddListener(() => ResetGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenHidePopUp(approvalPopUp, true);
        }
    }

    public void SetFailPopUp()
    {
        OpenHidePopUp(failWinPopUp, true);
        youWinFailText.text = "You lose!";
    }

    public void SetWinPopUp()
    {
        OpenHidePopUp(failWinPopUp, true);
        youWinFailText.text = "You win!";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMaskCheck.IsInLayerMask(other.gameObject, playerMask)) 
        {
            SetWinPopUp();
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void BlockPlayerMovement(bool value)
    {
        PlayerMovement.isBlocked = value;
    }

    private void OpenHidePopUp(GameObject popUp, bool value)
    {
        popUp.SetActive(value);
        BlockPlayerMovement(value);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
