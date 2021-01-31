using UnityEngine;

public class ConveyorTileDynamic : MonoBehaviour
{
    public Vector2Int GridCoords;

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
}