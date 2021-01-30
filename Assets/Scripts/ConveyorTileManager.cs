using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorTileManager : MonoBehaviour, IConveyorTileLookup
{
    public Tilemap ConveyorTilemap;

    void Start()
    {
        Tilemap tilemap = ConveyorTilemap;

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
    }

    public string GetActiveTile(Vector2Int position)
    {
        return null;
    }

    public string GetCardinalTile(Vector2Int position, Direction direction)
    {
        return null;
    }
}
