using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInteraction : MonoBehaviour
{
    private BoxCollider2D BoxTrigger;

    private Item HeldItem;

    private Vector3 ItemDropOffset;

    void Start()
    {
        BoxTrigger = GetComponent<BoxCollider2D>();
        ItemDropOffset = BoxTrigger.offset;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (HeldItem == null)
            {
                List<Collider2D> itemsInRange = new List<Collider2D>();
                int itemsInRangeCount = BoxTrigger.OverlapCollider(new ContactFilter2D(), itemsInRange);

                Debug.Assert(itemsInRangeCount <= 1);

                if (itemsInRangeCount == 1)
                {
                    Item itemInRange = itemsInRange[0].GetComponent<Item>();
                    HeldItem = itemInRange;
                    itemInRange.OnPickedUp();
                }
            }
            else
            {
                HeldItem.transform.position = transform.position + transform.rotation * ItemDropOffset;
                HeldItem.OnPutDown();
                HeldItem = null;
            }
        }
    }
}
