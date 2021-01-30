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

        Direction currentTileExitDirection = GetExitDirection(currentTileAction);

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
    }

    Direction GetExitDirection(string tileAction)
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
            default:
                Debug.Assert(false);
                return Direction.Up;
        }
    }
    
}
