using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    [Header("Start Position")]
    [SerializeField] private Transform startPosition;

    [Header("Pop ups")]
    [SerializeField] private GameObject failWinPopUp;
    [SerializeField] private Text youWinFailText;
    [SerializeField] private Button restartButton;
    [Space(10)]
    [SerializeField] private GameObject approvalPopUp;
    [SerializeField] private GameObject infoPopUp;

    [Header("Timer and Death Count")]
    [SerializeField] private Text deathCountText;
    [SerializeField] private Text timerText;

    private CharacterController player;
    private bool hasWon;
    private int deathCount;

    private void Start()
    {
        failWinPopUp.SetActive(false);
        approvalPopUp.SetActive(false);
        OpenHidePopUp(infoPopUp, true);

        hasWon = false;
        deathCount = 0;
        deathCountText.text = "Death Count: " + deathCount;

        player = FindObjectOfType<CharacterController>();

        ResetGame();
        restartButton.onClick.AddListener(() => ResetGame());

        StartTimer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenHidePopUp(approvalPopUp, true);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        int timeInSec = 0;
        timerText.text = "00:00";

        while (!hasWon)
        {
            yield return new WaitForSeconds(1);
            timeInSec++;

            int minutes = timeInSec / 60;
            int seconds = timeInSec % 60;

            string minutesText = minutes < 10 ? "0" + minutes : minutes.ToString();
            string secondsText = seconds < 10 ? "0" + seconds : seconds.ToString();

            timerText.text = minutesText + ":" + secondsText;
        }
    }

    public void SetFailPopUp()
    {
        OpenHidePopUp(failWinPopUp, true);
        youWinFailText.text = "You lose!";

        deathCount++;
        deathCountText.text = "Death Count: " + deathCount;
    }

    public void SetWinPopUp()
    {
        OpenHidePopUp(failWinPopUp, true);
        youWinFailText.text = "You win!";
        hasWon = true;
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
        player.enabled = false;
        player.transform.position = startPosition.position;
        player.enabled = true;
        PlayerMovement.isBlocked = false;

        if (hasWon)
        {
            hasWon = false;
            StartTimer();
        }
    }
}
