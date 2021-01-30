using UnityEngine;

[ExecuteInEditMode]
public class EndNode : MonoBehaviour
{
    public ItemColour ValidColor;

    private SpriteRenderer _renderer;

    private WinLossTracker WinLossTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        WinLossTracker = FindObjectOfType<WinLossTracker>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            if(item.Colour == ValidColor)
            {
                WinLossTracker.OnSuccess();
            }
            else
            {
                WinLossTracker.OnFailure();
            }

            item.gameObject.SetActive(false);
        }
    }
}
