using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossTracker : MonoBehaviour
{
    public int SuccessesToWin = 1;
    private int FailuresToLose = 3;

    private int Successes;
    private int Failures;

    private RectTransform WinPopup;
    private RectTransform LosePopup;
    private RectTransform CompletionPopup;
    private RectTransform Background;

    public int GetSuccesses()
    {
        return Successes;
    }

    public int GetSuccessToWin()
    {
        return SuccessesToWin;
    }

    public int GetFailures()
    {
        return Failures;
    }

    void Start()
    {
        Debug.Assert(SuccessesToWin > 0 && FailuresToLose > 0);

        WinPopup = transform.Find("Background/WinPopup").GetComponent<RectTransform>();
        LosePopup = transform.Find("Background/LossPopup").GetComponent<RectTransform>();
        CompletionPopup = transform.Find("Background/CompletionPopup").GetComponent<RectTransform>();
        Background = transform.Find("Background").GetComponent<RectTransform>();

        WinPopup.gameObject.SetActive(false);
        LosePopup.gameObject.SetActive(false);
        CompletionPopup.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (WinPopup.gameObject.activeInHierarchy)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                int levelIndex;
                if (int.TryParse(currentScene, out levelIndex))
                {
                    levelIndex++;
                    SceneManager.LoadScene(levelIndex.ToString());
                }
            }
            else if (LosePopup.gameObject.activeInHierarchy || CompletionPopup.gameObject.activeInHierarchy)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void OnSuccess()
    {
        Successes++;

        if (Successes >= SuccessesToWin)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int levelIndex;
            if (int.TryParse(currentScene, out levelIndex))
            {
                if(levelIndex < 10)
                {
                    Background.gameObject.SetActive(true);
                    WinPopup.gameObject.SetActive(true);
                }
                else
                {
                    Background.gameObject.SetActive(true);
                    CompletionPopup.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnFailure()
    {
        Failures++;

        if (Failures >= FailuresToLose)
        {
            Background.gameObject.SetActive(true);
            LosePopup.gameObject.SetActive(true);
        }
    }
}
