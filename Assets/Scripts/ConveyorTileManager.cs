using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorTileManager : MonoBehaviour, IConveyorTileLookup
{
    private Dictionary<string, ConveyorTile> _tileMap = new Dictionary<string, ConveyorTile>();
    
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

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
                    _tileMap.Add(GetCoordsKey(x,y), new ConveyorTile(x,y,tile.name));
                }
            }
        }
    }

    private string GetCoordsKey(int x, int y)
    {
        return $"{x}_{y}";
    }

    public string GetTileActionIdentifier(Vector2Int position)
    {
        // Find tile
        var key = GetCoordsKey(position.x, position.y);
        _tileMap.TryGetValue(key, out ConveyorTile tile);

        // Return action
        if(tile != null)
        {
            // Check basic directions
            var cardinalDirections = new string[] { "Up", "Right", "Down", "Left" };

            foreach(var direction in cardinalDirections)
            {
                if (tile.Name.Contains(direction))
                {
                    return direction;
                }
            }

            // Otherwise return tile name directly
            return tile.Name;
        }
        else
        {
            return null;
        }
    }

    public class ConveyorTile
    {
        public int X;
        public int Y;
        public string Name;

        public ConveyorTile(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }
    }
}
