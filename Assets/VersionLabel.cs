using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class VersionLabel : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = $"v{Application.version}";
    }
}
