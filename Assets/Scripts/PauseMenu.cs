using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuContainer;

    private RectTransform ResumeButton;
    private RectTransform QuitToMenuButton;
    private RectTransform QuitToDesktopButton;

    private RectTransform SelectedOption;
    private bool _showMenu = false;

    public bool IgnoreFirstEsc = false;
    private bool FirstEscIgnored = false;

    private void Start()
    {
        ResumeButton = transform.Find("Background/ResumeButton").GetComponent<RectTransform>();
        QuitToMenuButton = transform.Find("Background/QuitToMenuButton").GetComponent<RectTransform>();
        QuitToDesktopButton = transform.Find("Background/QuitToDesktopButton").GetComponent<RectTransform>();

        Debug.Assert(ResumeButton && QuitToMenuButton && QuitToDesktopButton);

        SelectedOption = ResumeButton;

        ResumeButton.Find("SelectedIndicators").gameObject.SetActive(true);
        QuitToMenuButton.Find("SelectedIndicators").gameObject.SetActive(false);
        QuitToDesktopButton.Find("SelectedIndicators").gameObject.SetActive(false);
    }

    public void SetPauseMenuState(bool paused)
    {
        _showMenu = paused;
        Time.timeScale = paused ? 0.0f : 1.0f;
        MenuContainer.SetActive(paused);

}

public void Update()
    {
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 7"))
        {
            if(IgnoreFirstEsc && !FirstEscIgnored)
            {
                FirstEscIgnored = true;
            }
            else
            {
                SetPauseMenuState(!_showMenu);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0)
        {
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(false);
            if (SelectedOption == ResumeButton)
            {
                SelectedOption = QuitToDesktopButton;
            }
            else if (SelectedOption == QuitToMenuButton)
            {
                SelectedOption = ResumeButton;
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                SelectedOption = QuitToMenuButton;
            }
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0)
        {
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(false);
            if (SelectedOption == ResumeButton)
            {
                SelectedOption = QuitToMenuButton;
            }
            else if (SelectedOption == QuitToMenuButton)
            {
                SelectedOption = QuitToDesktopButton;
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                SelectedOption = ResumeButton;
            }
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            if (SelectedOption == ResumeButton)
            {
                OnResumeClicked();
            }
            else if (SelectedOption == QuitToMenuButton)
            {
                QuitToMenuClicked();
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                OnQuitToDesktopClicked();
            }
        }
    }

    private void OnResumeClicked()
    {
        SetPauseMenuState(false);
    }

    public void QuitToMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitToDesktopClicked()
    {
        Application.Quit();
    }
}
