using UnityEngine;

[ExecuteInEditMode]
public class EndNode : MonoBehaviour
{
    public ItemColour ValidColor;

    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        if (ValidColor)
        {
            ColorUtility.TryParseHtmlString($"#{ValidColor.Hex}", out Color color);
            _renderer.color = color;
        }
    }
}
