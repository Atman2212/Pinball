using UnityEngine;

public class PlungerExitTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Ball"))
        {
            triggered = true;
            PinballGameManager.Instance.OnBallLaunched();  // Moved here from Plunger.cs
        }
    }

    public void ResetTrigger()
    {
        triggered = false;
    }
}
