using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool isHeld = false;
    private HingeJoint2D hinge;
    [SerializeField] private bool isLeftFlipper;

    public void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }
    public void Flick()
    {
        if (isHeld) return; // Already flicked

        isHeld = true;
        Debug.Log($"{name} flicked!");
        // TODO: Add flick animation or motor control here
        hinge.useMotor = true;
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = isLeftFlipper ? -300 : 300;
        hinge.motor = motor;
    }

    public void Release()
    {
        if (!isHeld) return; // Already released

        isHeld = false;
        Debug.Log($"{name} released!");
        // TODO: Add release animation or stop motor here
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = isLeftFlipper ? 300 : -300;
        hinge.motor = motor;
    }
}
