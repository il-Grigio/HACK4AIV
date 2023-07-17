using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Devices { Keyboard, Xbox, PS };

public class InputDeviceCheck : MonoBehaviour
{
    static Devices NewInput;
    static Devices CurrentInput;
    static bool IsController;

    public bool HasReciver = true;

    public delegate void InputDelegate(Devices type);

    public event InputDelegate OnInputChanged;

    private void Start()
    {
        CurrentInput = NewInput;

        if (HasReciver)
        {
            if (OnInputChanged != null) {
                OnInputChanged(CurrentInput);
            }
        }

    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.JoystickButton0) ||
                Input.GetKey(KeyCode.JoystickButton1) ||
                Input.GetKey(KeyCode.JoystickButton2) ||
                Input.GetKey(KeyCode.JoystickButton3) ||
                Input.GetKey(KeyCode.JoystickButton4) ||
                Input.GetKey(KeyCode.JoystickButton5) ||
                Input.GetKey(KeyCode.JoystickButton6) ||
                Input.GetKey(KeyCode.JoystickButton7) ||
                Input.GetKey(KeyCode.JoystickButton8) ||
                Input.GetKey(KeyCode.JoystickButton9) ||
                Input.GetKey(KeyCode.JoystickButton10) ||
                Input.GetKey(KeyCode.JoystickButton11) ||
                Input.GetKey(KeyCode.JoystickButton12) ||
                Input.GetKey(KeyCode.JoystickButton13) ||
                Input.GetKey(KeyCode.JoystickButton14) ||
                Input.GetKey(KeyCode.JoystickButton15) ||
                Input.GetKey(KeyCode.JoystickButton16) ||
                Input.GetKey(KeyCode.JoystickButton17) ||
                Input.GetKey(KeyCode.JoystickButton18) ||
                Input.GetKey(KeyCode.JoystickButton19))
            {
                IsController = true;
                Debug.Log("Controller key");
            }
            else
            {
                IsController = false;
                Debug.Log("Keyboard key");
            }
        }

        if (IsController)
        {
            if (Input.GetJoystickNames()[0].Contains("Xbox") || Input.GetJoystickNames()[1].Contains("Xbox") || Input.GetJoystickNames()[2].Contains("Xbox"))
            {
                NewInput = Devices.Xbox;
            }
            else
            {
                NewInput = Devices.PS;
            }

        }
        else
        {
            NewInput = Devices.Keyboard;

        }

        if (NewInput != CurrentInput)
        {
            CurrentInput = NewInput;

            if (HasReciver && OnInputChanged != null)
            {
                OnInputChanged(CurrentInput);
                Debug.Log("input change detected");
            }
        }
    }
}
