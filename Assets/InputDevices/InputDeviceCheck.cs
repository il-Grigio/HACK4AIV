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
            if (Input.GetKey(KeyCode.Joystick1Button0) ||
                Input.GetKey(KeyCode.Joystick1Button1) ||
                Input.GetKey(KeyCode.Joystick1Button2) ||
                Input.GetKey(KeyCode.Joystick1Button3) ||
                Input.GetKey(KeyCode.Joystick1Button4) ||
                Input.GetKey(KeyCode.Joystick1Button5) ||
                Input.GetKey(KeyCode.Joystick1Button6) ||
                Input.GetKey(KeyCode.Joystick1Button7) ||
                Input.GetKey(KeyCode.Joystick1Button8) ||
                Input.GetKey(KeyCode.Joystick1Button9) ||
                Input.GetKey(KeyCode.Joystick1Button10) ||
                Input.GetKey(KeyCode.Joystick1Button11) ||
                Input.GetKey(KeyCode.Joystick1Button12) ||
                Input.GetKey(KeyCode.Joystick1Button13) ||
                Input.GetKey(KeyCode.Joystick1Button14) ||
                Input.GetKey(KeyCode.Joystick1Button15) ||
                Input.GetKey(KeyCode.Joystick1Button16) ||
                Input.GetKey(KeyCode.Joystick1Button17) ||
                Input.GetKey(KeyCode.Joystick1Button18) ||
                Input.GetKey(KeyCode.Joystick1Button19))
            {
                IsController = true;
            }
            else
            {
                IsController = false;
            }
        }

        if (IsController)
        {
            if (Input.GetJoystickNames()[0].Contains("Xbox"))
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

            if (HasReciver)
            {
                OnInputChanged(CurrentInput);
            }
        }
    }
}
