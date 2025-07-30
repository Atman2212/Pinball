using UnityEngine;

public class PinballGameManager : MonoBehaviour
{
    public static PinballGameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject plungerBlocker;
    [SerializeField] private PlungerExitTrigger plungerExitTrigger;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnBallLaunched()
    {
        Invoke(nameof(EnablePlungerBlocker), 2f);
    }

    public void OnBallReset()
    {
        DisablePlungerBlocker();
        plungerExitTrigger.ResetTrigger();
    }

    private void EnablePlungerBlocker()
    {
        plungerBlocker.SetActive(true);
    }

    private void DisablePlungerBlocker()
    {
        plungerBlocker.SetActive(false);
    }
}
