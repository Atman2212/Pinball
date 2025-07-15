using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class FlipperInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private Dictionary<PinballAction, Flipper> flippers;

    private void Awake()
    {
        flippers = new Dictionary<PinballAction, Flipper>();

        foreach (var flipper in GetComponentsInChildren<Flipper>())
        {
            if (flipper.name.Contains("Left"))
                flippers[PinballAction.LEFT_FLIPPER] = flipper;
            else if (flipper.name.Contains("Right"))
                flippers[PinballAction.RIGHT_FLIPPER] = flipper;
        }

        PinballInputLayer.Init();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in flippers)
        {
            if (PinballInputLayer.IsHeld(pair.Key))
            {
                pair.Value.Flick();
            }
            else
            {
                pair.Value.Release();
            }
        }
    }
}
