using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEPrompt : MonoBehaviour
{
    private PlayerCharacterMovement CharacterMovement;

    private bool _down;

    private void Start()
    {
        CharacterMovement = FindObjectOfType<PlayerCharacterMovement>();
        Debug.Assert(CharacterMovement);

        CharacterMovement.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            _down = true;
        }

        if (_down && (Input.GetKeyUp(KeyCode.A) || Input.GetButtonUp("joystick button 0")))
        {
            CharacterMovement.enabled = true;
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}
