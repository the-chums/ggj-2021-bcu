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
    string GetTileActionIdentifier(Vector2Int position);
}
