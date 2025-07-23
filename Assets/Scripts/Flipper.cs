using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool isHeld = false;
    private HingeJoint2D hinge;

    [Header("Flipper Settings")]
    [SerializeField] private bool isLeftFlipper;
    [SerializeField] private float motorSpeed = 400f;
    [SerializeField] private float holdSpeed = 200f; // speed when holding in rest position
    [SerializeField] private float maxTorque = 3000f;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    public void Flick()
    {
        if (isHeld) return;

        isHeld = true;
        hinge.useMotor = true;
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = isLeftFlipper ? -motorSpeed : motorSpeed;
        motor.maxMotorTorque = maxTorque;
        hinge.motor = motor;
    }

    public void Release()
    {
        if (!isHeld) return;

        isHeld = false;
        hinge.useMotor = true;

        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = isLeftFlipper ? holdSpeed : -holdSpeed;  // Gently push back to rest
        motor.maxMotorTorque = maxTorque;
        hinge.motor = motor;
    }

    private void OnValidate()
    {
        if (motorSpeed < 0)
        {
            Debug.LogWarning("Motor speed should be positive. Converting to positive.");
            motorSpeed = Mathf.Abs(motorSpeed);
        }

        if (holdSpeed < 0)
        {
            Debug.LogWarning("Hold speed should be positive. Converting to positive.");
            holdSpeed = Mathf.Abs(holdSpeed);
        }
    }
}
