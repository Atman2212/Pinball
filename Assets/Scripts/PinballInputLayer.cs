using UnityEngine;

public static class PinballInputLayer
{
    private static InputSystem_Actions input;

    public static void Init()
    {
        input = new InputSystem_Actions();
        input.Enable();
    }

    public static bool IsHeld(PinballAction action)
    {
        switch (action)
        {
            case PinballAction.LEFT_FLIPPER:
                return input.Controls.LeftFlipper.ReadValue<float>() > 0.5f;
            case PinballAction.RIGHT_FLIPPER:
                return input.Controls.RightFlipper.ReadValue<float>() > 0.5f;
            // Add more cases as needed
            default:
                return false;
        }
    }
}
