using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    WinLossTracker WinLossTracker;

    RectTransform ProgressBarRect;

    public Text SuccessText;

    public GameObject[] FailureIcons;

    public Color NonFailureColor;
    public Color FailureColor;

    int SuccessesToWin;

    float ParentRectWidth;

    // Start is called before the first frame update
    void Start()
    {
        WinLossTracker = FindObjectOfType<WinLossTracker>();
        SuccessesToWin = WinLossTracker.GetSuccessToWin();
        ProgressBarRect = GetComponent<RectTransform>();
        ParentRectWidth = ProgressBarRect.parent.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        int successes = WinLossTracker.GetSuccesses();
        int failures = WinLossTracker.GetFailures();
        Vector2 newRectSize = ProgressBarRect.sizeDelta;
        newRectSize.x = successes * (ParentRectWidth / SuccessesToWin);
        ProgressBarRect.sizeDelta = newRectSize;
        SuccessText.text = string.Format("{0}/{1}", successes, SuccessesToWin);

        for(int i = 0; i < 3; i++)
        {
            FailureIcons[i].GetComponent<Image>().color = i >= failures ? NonFailureColor : FailureColor;
        }
    }
}
