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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        controlSquare.GetComponent<Animator>().SetBool("isEnabled", squareIsEnabled);
        controlX.GetComponent<Animator>().SetBool("isEnabled", xIsEnabled);
        controlTriangle.GetComponent<Animator>().SetBool("isEnabled", triangleIsEnabled);
        controlCircle.GetComponent<Animator>().SetBool("isEnabled", circleIsEnabled);
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