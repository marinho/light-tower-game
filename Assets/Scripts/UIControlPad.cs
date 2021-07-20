using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlPad : MonoBehaviour
{
    [SerializeField] GameObject controlSquare;
    [SerializeField] GameObject controlX;
    [SerializeField] GameObject controlTriangle;
    [SerializeField] GameObject controlCircle;

    private bool squareIsEnabled = false;
    private bool xIsEnabled = false;
    private bool triangleIsEnabled = true; // Jump is always enabled
    private bool circleIsEnabled = false;

    void Update()
    {
        int keyboardFactor = UniversalInput.Instance.GetUsingKeyboard() ? 2 : 0;
        int isOff = 1 + keyboardFactor;
        int isOn = 2 + keyboardFactor;
        controlSquare.GetComponent<Animator>().SetInteger("state", squareIsEnabled ? isOn : isOff);
        controlX.GetComponent<Animator>().SetInteger("state", xIsEnabled ? isOn : isOff);
        controlTriangle.GetComponent<Animator>().SetInteger("state", triangleIsEnabled ? isOn : isOff);
        controlCircle.GetComponent<Animator>().SetInteger("state", circleIsEnabled ? isOn : isOff);
    }

    public void SetSquareEnabled(bool enable) {
        squareIsEnabled = enable;
    }

    public void SetXEnabled(bool enable) {
        xIsEnabled = enable;
    }

    public void SetTriangleEnabled(bool enable) {
        triangleIsEnabled = enable;
    }

    public void SetCircleEnabled(bool enable) {
        circleIsEnabled = enable;
    }
}
