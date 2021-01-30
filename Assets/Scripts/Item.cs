using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer Sprite;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPickedUp()
    {
        Sprite.enabled = false;
    }

    public void OnPutDown()
    {
        Sprite.enabled = true;
    }
}
