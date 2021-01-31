using UnityEngine;

public class InteractableLever : MonoBehaviour
{
    public Vector2Int LinkedConveyorGridCoords;

    public ConveyorTileManager ConveyorManager;
    public GameObject ConveyorDirectionIcon;

    private float _currentRotation = 0;
    private SpriteRenderer _renderer;
    private Vector2Int GridOffset = new Vector2Int(19, 12);
    private Vector2Int ConveyorGridSpace;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        ConveyorGridSpace = new Vector2Int(GridOffset.x + LinkedConveyorGridCoords.x, GridOffset.y + LinkedConveyorGridCoords.y);
    }

    public void Operate()
    {
        _renderer.flipX = !_renderer.flipX;
        ConveyorManager.Interact(ConveyorGridSpace);
        ConveyorDirectionIcon.transform.Rotate(0, 0, -90);
    }
}
