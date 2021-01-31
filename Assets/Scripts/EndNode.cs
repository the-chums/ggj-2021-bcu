﻿using UnityEngine;
using System;


public class EndNode : MonoBehaviour
{
    public ItemColour ValidColor;
    public SpriteRenderer HeartRenderer;
    public Customer Customer;
    public Boolean Destory;

    private SpriteRenderer _renderer;
    private WinLossTracker WinLossTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        WinLossTracker = FindObjectOfType<WinLossTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        if (ValidColor)
        {
            ColorUtility.TryParseHtmlString($"#{ValidColor.Hex}", out Color color);

            if (_renderer)
            {
                _renderer.color = color;
                HeartRenderer.color = color;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            if(!Destory && item.Colour == ValidColor)
            {
                WinLossTracker.OnSuccess();
                Customer.ChangeCustomer();
            }
            if (Destory)
            {
                Debug.Log("wheyHEY!!!");
            }
            else
            {
                WinLossTracker.OnFailure();
            }

            item.gameObject.SetActive(false);
        }
    }
}
