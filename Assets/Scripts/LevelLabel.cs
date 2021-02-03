using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LevelLabel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = $"LVL {SceneManager.GetActiveScene().name}";
    }
}
