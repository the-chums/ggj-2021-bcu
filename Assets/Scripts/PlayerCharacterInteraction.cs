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
            List<Collider2D> collidersInRange = new List<Collider2D>();
            int collidersInRangeCount = BoxTrigger.OverlapCollider(new ContactFilter2D(), collidersInRange);

            if (HeldItem == null)
            {
                if (collidersInRangeCount == 0)
                {
                    return;
                }

                foreach (Collider2D colliderInRange in collidersInRange)
                {
                    Item itemInRange = colliderInRange.GetComponent<Item>();

                    if (itemInRange)
                    {
                        HeldItem = itemInRange;
                        itemInRange.OnPickedUp();
                    }
                }
            }
            else
            {
                foreach (Collider2D colliderInRange in collidersInRange)
                {
                    var tilemapInRange = colliderInRange.GetComponent<UnityEngine.Tilemaps.TilemapCollider2D>();

                    if (tilemapInRange)
                    {
                        var charPos = transform.position;
                        var targetPos = charPos + transform.forward;
                        var targetGridPos = WorldToGridPos(targetPos);

                        //HeldItem = itemInRange;
                        //itemInRange.OnPickedUp();
                    }
                }

                //HeldItem.transform.position = transform.position + transform.rotation * ItemDropOffset;
                //HeldItem.OnPutDown();
                HeldItem = null;
            }
        }
    }

    private Vector2 WorldToGridPos(Vector2 position)
    {
        return position;
    }
}
