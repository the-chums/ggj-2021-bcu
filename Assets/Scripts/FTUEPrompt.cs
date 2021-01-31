using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEPrompt : MonoBehaviour
{
    private PlayerCharacterMovement CharacterMovement;

    private void Start()
    {
        CharacterMovement = FindObjectOfType<PlayerCharacterMovement>();
        Debug.Assert(CharacterMovement);

        CharacterMovement.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            CharacterMovement.enabled = true;
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}
