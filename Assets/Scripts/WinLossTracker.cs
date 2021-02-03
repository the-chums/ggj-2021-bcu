using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class WinLossTracker : MonoBehaviour
{
    public int SuccessesToWin = 1;
    private int FailuresToLose = 3;

    private Dictionary<string, object> analytics_event = new Dictionary<string,object>();

    public GameObject ParcelHead;
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
        int Level = Convert.ToInt32(SceneManager.GetActiveScene().name);
        analytics_event.Add( "level_number", Level);
        analytics_event.Add("deliveries_to_pass", SuccessesToWin);
        analytics_event.Add( "max_fails", FailuresToLose);
        analytics_event.Add("start_items", GetAllItems());
        analytics_event.Add("start_blue", GetItemCount("blue"));
        analytics_event.Add("start_green", GetItemCount("green"));
        analytics_event.Add("start_purple", GetItemCount("purple"));
        analytics_event.Add("start_red", GetItemCount("red"));
        analytics_event.Add("start_yellow", GetItemCount("yellow"));
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
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
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

        if (Successes >= SuccessesToWin && !LosePopup.gameObject.activeInHierarchy)
        {
            analytics_event.Add( "end_items", GetAllItems());
            analytics_event.Add("end_blue", GetItemCount("blue"));
            analytics_event.Add("end_green", GetItemCount("green"));
            analytics_event.Add( "end_purple", GetItemCount("purple"));
            analytics_event.Add("end_red", GetItemCount("red"));
            analytics_event.Add( "end_yellow", GetItemCount("yellow"));
            analytics_event.Add("total_failures", Failures);
            analytics_event.Add("event_type", 1);
            AnalyticsEventFire();
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

        if (Failures >= FailuresToLose && !WinPopup.gameObject.activeInHierarchy && !CompletionPopup.gameObject.activeInHierarchy)
        {
            analytics_event.Add( "end_items", GetAllItems());
            analytics_event.Add( "end_blue", GetItemCount("blue"));
            analytics_event.Add("end_green", GetItemCount("green"));
            analytics_event.Add("end_purple", GetItemCount("purple"));
            analytics_event.Add("end_red", GetItemCount("red"));
            analytics_event.Add("end_yellow", GetItemCount("yellow"));
            analytics_event.Add("total_failures", Failures);
            analytics_event.Add("event_type", 0);
            AnalyticsEventFire();
            Background.gameObject.SetActive(true);
            LosePopup.gameObject.SetActive(true);
        }
    }

    private int GetAllItems()
    {
        
        Transform[] Parcels = ParcelHead.GetComponentsInChildren<Transform>();
        int item_count = 1;
        foreach (Transform child in Parcels)
        {
            if (child.gameObject.tag == "Parcels" && child.gameObject.GetComponent<Item>() != null)
            {
                item_count++;
            }
        }
        return item_count;
    }

    private int GetItemCount(string colour_to_check)
    {
        Transform[] Parcels = ParcelHead.GetComponentsInChildren<Transform>();
        int colour_count = 1;
        int item_count = 1;
        foreach (Transform child in Parcels)
        {
            if (child.gameObject.tag == "Parcels" && child.gameObject.GetComponent<Item>() != null)
            {
                item_count++;
                string parcel_colour = Convert.ToString(child.gameObject.GetComponent<Item>().item_colour).ToLower();
                if (parcel_colour == colour_to_check)
                {
                    colour_count++;
                }
            }
        }
        return colour_count;
    }

    public void AnalyticsEventFire()
    {
        AnalyticsResult result = Analytics.CustomEvent("level_end", analytics_event);
        Debug.Log(result);
    }
}
