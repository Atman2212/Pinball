using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool isHeld = false;
    private HingeJoint2D hinge;

    [Header("Flipper Settings")]
    [SerializeField] private bool isLeftFlipper;
    [SerializeField] private float motorSpeed = 400f;
    public void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }
    public void Flick()
    {
        if (isHeld) return; // Already flicked

        isHeld = true;
        hinge.useMotor = true;
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = isLeftFlipper ? -motorSpeed : motorSpeed;
        hinge.motor = motor;
    }

    public void Release()
    {
        if (!isHeld) return;
        isHeld = false;

        JointMotor2D motor = hinge.motor;

        // Hold it in resting position with motor
        motor.motorSpeed = isLeftFlipper ? motorSpeed : -motorSpeed;  // ← set this to match your flipper's rest angle
        hinge.motor = motor;
        hinge.useMotor = true;
    }

    // This method runs in the Unity Editor when you change values
    void OnValidate()
    {
        if (motorSpeed < 0)
        {
            Debug.LogWarning("Motor speed should be positive. Converting to positive.");
            motorSpeed = Mathf.Abs(motorSpeed);
        }
    }
}