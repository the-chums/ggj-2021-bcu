using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItemMover : MonoBehaviour
{
    public float TickRate = 1;

    private List<Item> ItemsOnConveyor;

    private float CurrentTick;

    private ConveyorTileManager ConveyorTileManager;

    void Start()
    {
        ConveyorTileManager = GetComponent<ConveyorTileManager>();

        ItemsOnConveyor = new List<Item>();

        Debug.Assert(ConveyorTileManager);
    }

    void Update()
    {
        CurrentTick += Time.deltaTime;

        if(CurrentTick < TickRate)
        {
            return;
        }

        while(CurrentTick > TickRate)
        {
            CurrentTick -= TickRate;
        }

        MoveItems();
    }

    void MoveItems()
    {
        foreach(Item item in ItemsOnConveyor)
        {
            MoveItem(item);
        }
    }

    void MoveItem(Item item)
    {
        Vector2Int itemPosition = item.GetPosition();

        string currentTileAction = ConveyorTileManager.GetTileActionIdentifier(itemPosition);
        ConveyorTileManager.TileState tileState = ConveyorTileManager.GetTileState(itemPosition);

        Direction currentTileExitDirection = GetExitDirection(currentTileAction, tileState, item);

        Vector2Int newPosition = itemPosition;
        
        switch(currentTileExitDirection)
        {
            case Direction.Up:
                newPosition.y++;
                break;
            case Direction.Right:
                newPosition.x++;
                break;
            case Direction.Down:
                newPosition.y--;
                break;
            case Direction.Left:
                newPosition.x--;
                break;
        }

        item.SetPosition(newPosition);

        if(tileState != null)
        {
            tileState.UpdateOnItemLeaving();
        }
    }

    Direction GetExitDirection(string tileAction, ConveyorTileManager.TileState tileState, Item item)
    {
        switch(tileAction)
        {
            case "up":
                return Direction.Up;
            case "right":
                return Direction.Right;
            case "down":
                return Direction.Down;
            case "left":
                return Direction.Left;
            case "binary_alternating_lr":
                {
                    var state = (ConveyorTileManager.AlternatingTileState)tileState;

                    if (state.Count % 2 == 0)
                    {
                        return Direction.Left;
                    }
                    else
                    {
                        return Direction.Right;
                    }
                }
            case "binary_alternating_lu":
                {
                    var state = (ConveyorTileManager.AlternatingTileState)tileState;

                    if (state.Count % 2 == 0)
                    {
                        return Direction.Left;
                    }
                    else
                    {
                        return Direction.Up;
                    }
                }
            case "binary_alternating_ru":
                {
                    var state = (ConveyorTileManager.AlternatingTileState)tileState;

                    if (state.Count % 2 == 0)
                    {
                        return Direction.Right;
                    }
                    else
                    {
                        return Direction.Up;
                    }
                }
            case "ternary_alternating":
                {
                    var state = (ConveyorTileManager.AlternatingTileState)tileState;
                    if (state.Count % 3 == 0)
                    {
                        return Direction.Left;
                    }
                    else if (state.Count % 3 == 1)
                    {
                        return Direction.Up;
                    }
                    else
                    {
                        return Direction.Right;
                    }
                }
            default:
                return Direction.None;
        }
    }

    public void AddItemToConveyor(Item item, Vector2Int position)
    {
        item.SetPosition(position);
        ItemsOnConveyor.Add(item);
    }
}
