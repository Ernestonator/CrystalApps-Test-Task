using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        failWinPopUp.SetActive(false);
        approvalPopUp.SetActive(false);
        OpenHidePopUp(infoPopUp, true);

        player = FindObjectOfType<CharacterController>();

        ResetGame();
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
        //SceneManager.LoadScene(0);
        player.Move(startPosition.position - player.transform.position);
        PlayerMovement.isBlocked = false;
    }
}
