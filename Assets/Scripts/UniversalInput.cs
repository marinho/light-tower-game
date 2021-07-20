using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalInput : Singleton<UniversalInput>
{
    // [SerializeField] Joystick mobileJoystick;

    [SerializeField] bool isMobile = false;
    bool isUsingKeyboard = true;

    bool keyboardVerified = false;

    void Awake()
    {
        // isMobile = SystemInfo.deviceType == DeviceType.Handheld;
        // if (SystemInfo.deviceType == DeviceType.Console)
        // if (SystemInfo.deviceType == DeviceType.Desktop)
    }

    void Update() {
        if (!keyboardVerified) {
            for (int i = 0;i < 20; i++) {
                if (Input.GetKeyDown("joystick 1 button " + i)){
                    isUsingKeyboard = false;
                }
            }
            keyboardVerified = true;
        }
    }

    public bool GetUsingKeyboard() {
        return isUsingKeyboard;
    }

    public float GetAxisVertical()
    {
        // return isMobile ? mobileJoystick.Vertical : Input.GetAxisRaw("Vertical");
        return Input.GetAxisRaw("Vertical");
    }

    public float GetAxisHorizontal()
    {
        // return isMobile ? mobileJoystick.Horizontal : Input.GetAxisRaw("Horizontal");
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetButton(string inputName)
    {
        return Input.GetButton(inputName);
    }

    public bool GetButtonUp(string inputName)
    {
        // return isMobile ? mobileJoystick.Horizontal : Input.GetAxisRaw("Horizontal");
        return Input.GetButtonUp(inputName);
    }

    public bool GetButtonDown(string inputName)
    {
        // return isMobile ? mobileJoystick.Horizontal : Input.GetAxisRaw("Horizontal");
        return Input.GetButtonDown(inputName);
    }
}
