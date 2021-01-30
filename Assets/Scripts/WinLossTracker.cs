﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossTracker : MonoBehaviour
{
    public int SuccessesToWin = 1;
    public int FailuresToLose = 3;

    private int Successes;
    private int Failures;

    void Start()
    {
        Debug.Assert(SuccessesToWin > 0 && FailuresToLose > 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            OnSuccess();
        }
        else if(Input.GetKeyDown(KeyCode.PageDown))
        {
            OnFailure();
        }

        Debug.Log("Successes - " + Successes + "/" + SuccessesToWin + "\tFailures - " + Failures + "/" + FailuresToLose);
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
                levelIndex++;
                SceneManager.LoadScene(levelIndex.ToString());
            }
        }
    }

    public void OnFailure()
    {
        Failures++;

        if (Failures >= FailuresToLose)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
