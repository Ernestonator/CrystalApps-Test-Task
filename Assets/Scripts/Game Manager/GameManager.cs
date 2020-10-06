using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Script responsible for game management.
/// Stores variables responsible for timer, death count, pop ups.
///
/// Here is decided if player won.
///
/// How To use it:
/// Drag GameManager prefab to the scene and set all ui elements,
/// like popups and texts.
/// Set start point wherever you want player to start.
/// Set GameManager Collider in place where is the finish line.
/// Make sure it's trigger.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    [Header("Start Position")]
    [SerializeField, Tooltip("Place where player starts the game.")] private Transform startPosition;

    [Header("Pop ups")]
    [SerializeField] private GameObject failWinPopUp;
    [SerializeField] private Text youWinFailText;
    [SerializeField] private Button restartGameButton;
    [Space(10)]
    [SerializeField] private GameObject approvalPopUp;
    [SerializeField] private GameObject infoPopUp;

    [Header("Timer and Death Count")]
    [SerializeField] private Text deathCountText;
    [SerializeField] private Text timerText;

    private CharacterController player;

    /// <summary>
    /// Defines if player has won the game.
    /// </summary>
    private bool hasWon;

    /// <summary>
    /// Counts how many player died.
    /// </summary>
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
        restartGameButton.onClick.AddListener(() => ResetGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenHidePopUp(approvalPopUp, true);
        }
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Resets and starts timer.
    /// Also it displays it on screen.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Sets Fail Pop Up Appearence
    /// Increases death counts.
    /// </summary>
    public void SetFailPopUp()
    {
        OpenHidePopUp(failWinPopUp, true);
        youWinFailText.text = "You lose!";

        deathCount++;
        deathCountText.text = "Death Count: " + deathCount;
    }

    /// <summary>
    /// Sets Win Pop up Appearence.
    /// </summary>
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

    /// <summary>
    /// Opens or hides popup
    /// Blocks player movement when pop up is enabled.
    /// </summary>
    /// <param name="popUp">pop up we want open or hide</param>
    /// <param name="value">defines if we want to hide or open the pop up</param>
    private void OpenHidePopUp(GameObject popUp, bool value)
    {
        popUp.SetActive(value);
        BlockPlayerMovement(value);
    }

    /// <summary>
    /// Resets Game by setting player position and reseting timer if needed.
    /// </summary>
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
