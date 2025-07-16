using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class GapCoverHandler : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float duration      = 3f;   // total life
    [SerializeField] private float flashWindow   = 1f;   // last X seconds flash faster

    [Header("Flashing")]
    [SerializeField] private Color flashColor    = Color.red;
    [SerializeField] private float baseFlashRate = 2f;   // flashes / sec at start of window
    [SerializeField] private float maxFlashRate  = 8f;   // flashes / sec at end of window

    private SpriteRenderer sr;
    private Color startColor;

    void Awake()
    {
        sr          = GetComponent<SpriteRenderer>();
        startColor  = sr.color;
        Invoke(nameof(BeginFlashing), duration - flashWindow);
        Invoke(nameof(RemoveCover),    duration);
    }

    void BeginFlashing()  => StartCoroutine(FlashRoutine());

    IEnumerator FlashRoutine()
    {
        float elapsed = 0f;
        while (elapsed < flashWindow)
        {
            // Flash rate accelerates from base → max over the window
            float t          = elapsed / flashWindow;
            float rateNow    = Mathf.Lerp(baseFlashRate, maxFlashRate, t);
            float ping       = Mathf.PingPong(Time.time * rateNow, 1f);   // 0→1→0 waveform
            sr.color         = Color.Lerp(startColor, flashColor, ping);

            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = startColor; // reset just before removal (optional)
    }

    void RemoveCover() => Destroy(gameObject);
}
