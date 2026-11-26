using UnityEngine;

public class WaterRipple_TESTING : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rippleSprite;

    [Header("Setting")]
    [SerializeField] private float startSize = 0.1f;
    [SerializeField] private float maxSize = 1f;
    [SerializeField] private float duration = 1f; //NOTE: Time it takes to get from min to max size
    [SerializeField] private float startDelay = 0f;

    [Header("Fade")]
    [SerializeField] private float fadeStart = 0.50f;
    [SerializeField] private float maxOpacity = 0.70f;

    private float timer;
    private float currentSize;
    private float currentOpacity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rippleSprite == null)
        {
            rippleSprite = GetComponent<SpriteRenderer>();
        }
    }


    void OnEnable()
    {
        RestartRipple();

        timer -= startDelay;

        if (startDelay > 0f)
        {
            ChangeOpacity(0f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (rippleSprite == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer < 0f)
        {
            ChangeOpacity(0f);
            return;
        }

        if (timer >= duration)
        {
            timer = timer % duration;
        }

        float progress = timer / duration;

        progress = Mathf.Clamp01(progress);

        currentSize = Mathf.Lerp(startSize, maxSize, progress);
        // Debug.Log("Ripple 1 Current Size; " + currentSize);

        transform.localScale = new Vector3(currentSize, currentSize, currentSize);

        if (progress < fadeStart)
        {
            currentOpacity = maxOpacity;
        }

        else
        {
            float fadeProgress = Mathf.InverseLerp(fadeStart, 1f, progress);

            currentOpacity = maxOpacity * (1f - fadeProgress);
        }


        Color rippleColor = rippleSprite.color;

        rippleColor.a = currentOpacity;
        rippleSprite.color = rippleColor;

    }

    private void RestartRipple()
    {
        timer = 0f;
        currentSize = startSize;
        currentOpacity = 1f;

        transform.localScale = new Vector3(startSize, startSize, startSize);

        ChangeOpacity(maxOpacity);
    }

    private void ChangeOpacity(float opacity)
    {
        if (rippleSprite == null)
        {
            return;
        }

        else
        {
            Color rippleColor = rippleSprite.color;
            rippleColor.a = opacity;
            rippleSprite.color = rippleColor;
        }
    }
}
