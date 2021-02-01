using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorTileManager : MonoBehaviour, IConveyorTileLookup
{
    public ConveyorTileDynamic[] DynamicTiles;
    
    private Dictionary<string, ConveyorTile> _tileMap = new Dictionary<string, ConveyorTile>();
    private Vector2Int GridOffset = new Vector2Int(19, 12);

    void Start()
    {
        initialiseTilemap();
    }

    private void initialiseTilemap()
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

                    ConveyorTileDynamic matchingDynamicTile = null;

                    if(DynamicTiles != null)
                    {
                        foreach (var dynamicTile in DynamicTiles)
                        {
                            if ((dynamicTile.GridCoords + GridOffset) == new Vector2Int(x, y))
                            {
                                matchingDynamicTile = dynamicTile;
                            }
                        }
                    }

                    var conveyorTile = new ConveyorTile(x, y, tile.name, matchingDynamicTile);

                    _tileMap.Add(GetCoordsKey(x, y), conveyorTile);
                }
            }
        }
    }

    private Dictionary<string, ConveyorTile> GetTilemap()
    {
        if(_tileMap.Count == 0)
        {
            initialiseTilemap();
        }

        return _tileMap;
    }

    private string GetCoordsKey(int x, int y)
    {
        return $"{x}_{y}";
    }

    public void Interact(Vector2Int position)
    {
        var tile = GetTile(position);

        if (tile != null)
        {
            tile.Next();
        }
    }

    public ConveyorTile GetTile(Vector2Int position)
    {
        // Find tile
        var key = GetCoordsKey(position.x, position.y);
        GetTilemap().TryGetValue(key, out ConveyorTile tile);

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
        public abstract void UpdateState();
        public abstract void UpdateDynamicTileState(Direction direction);
        public abstract void Next();
    };

    public class AlternatingTileState : TileState
    {
        public ConveyorTileDynamic _tileDynamic;
        public int Count;
        public override void Next() { }

        public AlternatingTileState(ConveyorTileDynamic tileDynamic) : base()
        {
            _tileDynamic = tileDynamic;
        }

        public override void UpdateState()
        {
            Count++;
        }

        public override void UpdateDynamicTileState(Direction nextDirection)
        {

            var angle = 0;
            switch(nextDirection)
            {
                case Direction.Left:
                    angle = 0;
                    break;
                case Direction.Up:
                    angle = 270;
                    break;
                case Direction.Right:
                    angle = 180;
                    break;
                case Direction.Down:
                    angle = 90;
                    break;
            }

            var rot = _tileDynamic.transform.rotation;
            _tileDynamic.transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, angle));
        }
    }

    public class ManualTileState : TileState
    {
        public int Rotation = 0;
        public override void Next() 
        {
            Rotation += 90;
            Rotation = Rotation % 360;
        }

        public override void UpdateState() { }
        public override void UpdateDynamicTileState(Direction direction) { }
    }

    public class ConveyorTile
    {
        public int X;
        public int Y;
        public string Name;
        public TileState State;

        public ConveyorTile(int x, int y, string name, ConveyorTileDynamic dynamicTile)
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
                    State = new AlternatingTileState(dynamicTile);
                    break;
                case "manual_switch":
                    State = new ManualTileState();
                    break;
                default:
                    break;
            }
        }

        public void Next()
        {
            Debug.Log("Next called");
            if (State != null)
            {
                State.Next();
            }
        }
    }
}
