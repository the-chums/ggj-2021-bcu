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

    // Given a point, what sort of tile is in the location
    string GetTileTypeIdentifier(Vector2Int position);
}
