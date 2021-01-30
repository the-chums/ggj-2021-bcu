using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public int XOffset;
    public int YOffset;

    public Vector2Int WorldToGridPos(Vector2 position)
    {
        float floorPosX = Mathf.Floor(position.x);
        float floorPosY = Mathf.Floor(position.y);
        return new Vector2Int((int)floorPosX + XOffset, (int)floorPosY + YOffset);
    }

    public Vector3 GridToWorldPos(Vector2Int position)
    {
        float floorPosX = position.x - XOffset + 0.5f;
        float floorPosY = position.y - YOffset + 0.5f;
        return new Vector3(floorPosX, floorPosY, 0);
    }
}
