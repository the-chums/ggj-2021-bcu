using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer Sprite;

    private Vector2Int Position;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPickedUp()
    {
        Sprite.enabled = false;
    }

    public void OnPutDown()
    {
        Sprite.enabled = true;
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
}
