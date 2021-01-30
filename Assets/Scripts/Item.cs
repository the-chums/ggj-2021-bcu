using UnityEngine;

[ExecuteInEditMode]
public class Item : MonoBehaviour
{
    public ItemColour Colour;
    public ItemType Type;
    
    public SpriteRenderer BackgroundSprite;

    private SpriteRenderer Sprite;
    private BoxCollider2D Collider;

    private Vector2Int Position;

    private bool PlacedOnConveyor = false;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
    }

    public void OnPickedUp()
    {
        transform.localPosition = Vector3.up * 0.75f;
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        Collider.enabled = false;
    }

    public void OnPutDown()
    {
        transform.SetParent(null);
        transform.localScale = new Vector3(1f, 1f, 1);
        Collider.enabled = true;
    }

    public Vector2Int GetPosition()
    {
        return Position;
    }

    public void SetPosition(Vector2Int newPosition)
    {
        Position = newPosition;
        transform.position = FindObjectOfType<GridHelper>().GridToWorldPos(newPosition);
    }

    public bool HasBeenPlacedOnConveyor()
    {
        return PlacedOnConveyor;
    }

    public void SetOnConveyor()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        PlacedOnConveyor = true;
    }

    void OnValidate()
    {
        if (Sprite)
        {
            Sprite.sprite = Type.Sprite;
        }

        if (BackgroundSprite != null && Colour != null)
        {
            ColorUtility.TryParseHtmlString($"#{Colour.Hex}", out Color color);
            BackgroundSprite.color = color;
        }
    }
}
