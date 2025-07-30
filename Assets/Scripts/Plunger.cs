using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Plunger : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float pullSpeed = 4f;
    [SerializeField] private float releaseSpeed = 20f;
    [SerializeField] private float maxPullDistance = 2f;

    private Vector2 startPos;
    private Rigidbody2D rb;
    private bool awaitingLaunch = false;
    private float currentPull = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
    }

    private void Update()
    {
        if (PinballInputLayer.IsHeld(PinballAction.PLUNGER))
        {
            awaitingLaunch = true;
            currentPull = Mathf.Min(currentPull + Time.deltaTime * pullSpeed, maxPullDistance);
            Vector2 pullPos = startPos - new Vector2(0, currentPull);
            rb.MovePosition(pullPos);
        }
        else if (awaitingLaunch)
        {
            awaitingLaunch = false;
            StartCoroutine(ReleasePlunger());
        }
    }

    private IEnumerator ReleasePlunger()
    {
        float releaseTime = 0.1f; // total duration
        float t = 0f;
        Vector2 currentPos = rb.position;
        Vector2 endPos = startPos;

        while (t < 1f)
        {
            t += Time.deltaTime / releaseTime;
            Vector2 newPos = Vector2.Lerp(currentPos, endPos, t);
            rb.MovePosition(newPos);
            yield return null;
        }

        currentPull = 0f;
    }
}
