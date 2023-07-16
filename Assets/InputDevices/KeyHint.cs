using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHint : MonoBehaviour
{
    public InputDeviceCheck DeviceScript;
    public Devices CurrentDevice;
    public Sprite[] InputSprite;
    public Image DisplayImage;
    public bool StartOff;

    private void Start()
    {
        if (StartOff)
        {
            Disabled();
        }
    }

    void OnEnable()
    {
        DeviceScript.OnInputChanged += InputChanged;
    }

    void OnDisable()
    {
        DeviceScript.OnInputChanged -= InputChanged;
    }

    private void InputChanged(Devices Device)
    {
        DisplayImage.sprite = InputSprite[(int)Device];

    }

    public void Enabled()
    {
        DisplayImage.color = new Color(1, 1, 1, 1);
    }

    public void Disabled()
    {
        DisplayImage.color = new Color(1, 1, 1, 0.2f);
    }


}
