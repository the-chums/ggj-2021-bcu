using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    public Sprite[] CustomerImages;
    public int ActiveIndex;
    public float AnimationChangeDuration;

    private SpriteRenderer _renderer;
    private bool _changing;
    private bool _customerLeft;

    private float _startPositionFactor = 5f;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float _startScaleFactor = 0.5f;
    private Vector3 _startScale;
    private Vector3 _endScale;

    private float _startAlpha = 0;
    private float _endAlpha;

    private float _animationStartTime;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _endScale = transform.localScale;
        _startScale = new Vector3(_endScale.x * _startScaleFactor, _endScale.y * _startScaleFactor, _endScale.z * _startScaleFactor);

        _startPosition = new Vector3(transform.position.x, transform.position.y + _startPositionFactor, transform.position.z);
        _endPosition = transform.position;

        _endAlpha = _renderer.color.a;

        configureFirstCustomer();
    }

    void Update()
    {
        var animElapsedTime = Time.time - _animationStartTime;

        if(AnimationChangeDuration == 0) { AnimationChangeDuration = 1; }
        var proportionElapsedTime = animElapsedTime / AnimationChangeDuration;

        // Check if animation active
        if(_changing)
        {
            if(!_customerLeft)
            {
                var calculatedAlpha = Mathf.Lerp(_endAlpha, _startAlpha, proportionElapsedTime);
                var activeColor = _renderer.color;
                _renderer.color = new Color(activeColor.r, activeColor.g, activeColor.b, calculatedAlpha);

                var calculatedScale = Vector3.Lerp(_endScale, _startScale, proportionElapsedTime);
                transform.localScale = calculatedScale;

                var calculatedPosition = Vector3.Lerp(_endPosition, _startPosition, proportionElapsedTime);
                transform.position = calculatedPosition;

                // Update animation state with current time
                _customerLeft = animElapsedTime > AnimationChangeDuration;

                if (_customerLeft)
                {
                    _animationStartTime = Time.time;
                
                    if (CustomerImages != null && CustomerImages.Length > 0)
                    {
                        ActiveIndex = Random.Range(0, CustomerImages.Length);
                        _renderer.sprite = CustomerImages[ActiveIndex];
                    }
                }
            }
            else
            {
                var calculatedAlpha = Mathf.Lerp(_startAlpha, _endAlpha, proportionElapsedTime);
                var activeColor = _renderer.color;
                _renderer.color = new Color(activeColor.r, activeColor.g, activeColor.b, calculatedAlpha);

                var calculatedScale = Vector3.Lerp(_startScale, _endScale, proportionElapsedTime);
                transform.localScale = calculatedScale;

                var calculatedPosition = Vector3.Lerp(_startPosition, _endPosition, proportionElapsedTime);
                transform.position = calculatedPosition;

                // Update animation state with current time
                _changing = animElapsedTime < AnimationChangeDuration;
            }
        }
    }

    private void configureFirstCustomer()
    {
        if (CustomerImages != null && CustomerImages.Length > 0)
        {
            ActiveIndex = Random.Range(0, CustomerImages.Length);
            _renderer.sprite = CustomerImages[ActiveIndex];
        }
    }

    public void ChangeCustomer()
    {
        _animationStartTime = Time.time;
        _changing = transform;
        _customerLeft = false;
    }
}
