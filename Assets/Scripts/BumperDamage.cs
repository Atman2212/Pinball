using UnityEngine;
using System.Collections;

public class BumperDamage : MonoBehaviour
{
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private int bonusDiceSides = 4;

    [Header("Feedback")]
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private float scalePopAmount = 1.2f;
    [SerializeField] private float popDuration = 0.1f;

    private AudioSource audioSource;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;

        int bonus = Random.Range(1, bonusDiceSides + 1);
        int total = baseDamage + bonus;

        PinballGameManager.Instance.AddDamage(total);
        StartCoroutine(ScalePopEffect());

        Debug.Log($"Bumper hit! Dealt {total} damage ({baseDamage} + d{bonusDiceSides})");

        if (audioSource != null && hitSFX != null)
            audioSource.PlayOneShot(hitSFX);

        // UI Damage Text (for Screen Space - Overlay canvas)
        if (PinballGameManager.Instance.damageTextUIPrefab != null)
        {
            Vector3 worldPos = transform.position;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            GameObject dmgTextObj = Instantiate(
                PinballGameManager.Instance.damageTextUIPrefab,
                PinballGameManager.Instance.worldCanvas.transform
            );

            dmgTextObj.GetComponent<RectTransform>().position = screenPos;

            DamageText dmgText = dmgTextObj.GetComponent<DamageText>();
            dmgText.Init(total);
        }
    }

    private IEnumerator ScalePopEffect()
    {
        transform.localScale = originalScale * scalePopAmount;
        yield return new WaitForSeconds(popDuration);
        transform.localScale = originalScale;
    }
}