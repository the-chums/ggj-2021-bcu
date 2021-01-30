using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInteraction : MonoBehaviour
{
    public GridHelper GridHelper;
    public ConveyorTileManager ConveyorTileManager;
    public BoxCollider2D InteractableBoxTrigger;

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
            int collidersInRangeCount = InteractableBoxTrigger.OverlapCollider(new ContactFilter2D(), collidersInRange);

            if (HeldItem == null)
            {
                Debug.Log("Nothing held, picking up");

                if (collidersInRangeCount == 0)
                {
                    return;
                }

                foreach (Collider2D colliderInRange in collidersInRange)
                {
                    Item itemInRange = colliderInRange.GetComponent<Item>();

                    if (itemInRange && !itemInRange.HasBeenPlacedOnConveyor())
                    {
                        HeldItem = itemInRange;
                        itemInRange.transform.SetParent(transform);
                        itemInRange.OnPickedUp();
                        break;
                    }
                }
            }
            else
            {
                var charPos = transform.position;
                var targetPos = charPos + transform.up;
                var targetGridPos = GridHelper.WorldToGridPos(targetPos);
                var onConveyor = false;

                if(collidersInRange != null && collidersInRange.Count > 0)
                {
                    foreach (Collider2D colliderInRange in collidersInRange)
                    {
                        var tilemapInRange = colliderInRange.GetComponent<UnityEngine.Tilemaps.TilemapCollider2D>();

                        if (tilemapInRange)
                        {
                            var gridTileType = ConveyorTileManager.GetTileTypeIdentifier(targetGridPos);

                            if (gridTileType != null && gridTileType.Contains("entry"))
                            {
                                onConveyor = true;
                                FindObjectOfType<ConveyorItemMover>().AddItemToConveyor(HeldItem, targetGridPos);
                                Debug.Log(targetGridPos + ", type: " + gridTileType);
                                break;
                            }
                            else
                            {
                                Debug.Log("not entry: " + targetGridPos + ", type: " + gridTileType);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Attempting to place, no colliders");
                }

                HeldItem.OnPutDown();
                
                if (onConveyor)
                {
                    HeldItem.SetOnConveyor();
                }
                else
                {
                    HeldItem.SetPosition(targetGridPos);
                }
                
                HeldItem = null;
            }
        }
    }
}
