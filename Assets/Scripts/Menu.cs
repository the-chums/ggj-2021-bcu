using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private RectTransform NewGameButton;
    private RectTransform CreditsButton;
    private RectTransform QuitToDesktopButton;

    private RectTransform SelectedOption;



    private void Start()
    {
        NewGameButton = transform.Find("Background/NewGameButton").GetComponent<RectTransform>();
        CreditsButton = transform.Find("Background/CreditsButton").GetComponent<RectTransform>();
        QuitToDesktopButton = transform.Find("Background/QuitToDesktopButton").GetComponent<RectTransform>();

        Debug.Assert(NewGameButton && CreditsButton && QuitToDesktopButton);

        SelectedOption = NewGameButton;

        NewGameButton.Find("SelectedIndicators").gameObject.SetActive(true);
        CreditsButton.Find("SelectedIndicators").gameObject.SetActive(false);
        QuitToDesktopButton.Find("SelectedIndicators").gameObject.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(false);
            if(SelectedOption == NewGameButton)
            {
                SelectedOption = QuitToDesktopButton;
            }
            else if (SelectedOption == CreditsButton)
            {
                SelectedOption = NewGameButton;
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                SelectedOption = CreditsButton;
            }
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(false);
            if (SelectedOption == NewGameButton)
            {
                SelectedOption = CreditsButton;
            }
            else if (SelectedOption == CreditsButton)
            {
                SelectedOption = QuitToDesktopButton;
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                SelectedOption = NewGameButton;
            }
            SelectedOption.Find("SelectedIndicators").gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            if(SelectedOption == NewGameButton)
            {
                OnNewGameClicked();
            }
            else if (SelectedOption == CreditsButton)
            {
                OnCreditsClicked();
            }
            else if (SelectedOption == QuitToDesktopButton)
            {
                OnQuitToDesktopClicked();
            }
        }
    }

    public void OnNewGameClicked()
    {
        SceneManager.LoadScene("1");
    }

    public void OnCreditsClicked()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnQuitToDesktopClicked()
    {
        Application.Quit();
    }
}
