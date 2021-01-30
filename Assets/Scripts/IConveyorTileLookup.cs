using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
};

public interface IConveyorTileLookup
{
    // Given a point, what are the tiles in the 4 cardinal directions and itself
    string GetActiveTile(Vector2Int position);

    // Map position and direction to an action:
    // Template: movement_tblr-movement_tblr
    string GetCardinalTile(Vector2Int position, Direction direction);
}
