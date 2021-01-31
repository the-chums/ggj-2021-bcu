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
                    //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    _tileMap.Add(GetCoordsKey(x, y), new ConveyorTile(x, y, tile.name));
                }
            }
        }
    }

    private string GetCoordsKey(int x, int y)
    {
        return $"{x}_{y}";
    }

    public ConveyorTile GetTile(Vector2Int position)
    {
        // Find tile
        var key = GetCoordsKey(position.x, position.y);
        _tileMap.TryGetValue(key, out ConveyorTile tile);

        // Return action
        if (tile != null)
        {
            return tile;
        }
        else
        {
            return null;
        }
    }
    public string GetTileTypeIdentifier(Vector2Int position)
    {
        var tile = GetTile(position);

        if (tile != null)
        {
            if(tile.Name.Contains("entry"))
            {
                return "entry";
            }
        }
     
        return null;
    }

    public string GetTileActionIdentifier(Vector2Int position)
    {
        var tile = GetTile(position);

        if (tile != null) {
            // Check basic directions
            var cardinalDirections = new string[] { "up", "right", "down", "left" };

            foreach (var direction in cardinalDirections)
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

    public TileState GetTileState(Vector2Int position)
    {
        var tile = GetTile(position);

        if (tile != null)
        {
            return tile.State;
        }
        else
        {
            return null;
        }
    }

    public abstract class TileState
    {
        public abstract void UpdateOnItemLeaving();
    };

    public class AlternatingTileState : TileState
    {
        public int Count;

        public override void UpdateOnItemLeaving()
        {
            Count++;
        }
    }

    public class ConveyorTile
    {
        public int X;
        public int Y;
        public string Name;
        public TileState State;

        public ConveyorTile(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;

            switch(name)
            {
                case "binary_alternating_lr":
                case "binary_alternating_lu":
                case "binary_alternating_ru":
                case "ternary_alternating":
                    State = new AlternatingTileState();
                    break;
                default:
                    break;
            }
        }
    }
}
