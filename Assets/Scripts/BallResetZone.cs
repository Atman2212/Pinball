using UnityEngine;

public class BallResetZone : MonoBehaviour
{
    [SerializeField] private Transform resetPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            other.transform.position = resetPoint.position;
            PinballGameManager.Instance.OnBallReset();
        }
    }
}
