using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        SceneManager.LoadScene("Rowan-POCScene");
    }

    public void OnCreditsClicked()
    {
        SceneManager.LoadScene("Credits");
    }
}
