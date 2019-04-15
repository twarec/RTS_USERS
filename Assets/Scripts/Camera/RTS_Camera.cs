using System;
using UnityEngine;
using YG_EventSystem;

[AddComponentMenu ("Camra/RTS_Camera")]
public class RTS_Camera : MonoBehaviour {
    private Camera camera;
    private Transform myTransform;
    private Transform cameraTransform;
    private bool ScrollPrees = false;
    private bool MouseRightPress = false;

    public bool VericalInver;
    public float Speed;
    public float SpeedRotate;
    [Range (0, 360)]
    public float MinXRotate, MaxXRotate;
    [Range (0, 1000)]
    public float MinYPosition, MaxYPosition;
    public UnityEngine.Events.UnityEvent PressEvent, DownEvent, UpEvent;

    private InputData[] inputDatas;
    private void OnEnable () {
        inputDatas = new InputData[] {
            new InputData ("Horizontal", HorizontalMove),
            new InputData ("Vertical", VerticalMove),
            new InputData ("Mouse X", MouseXMove),
            new InputData ("Mouse Y", MouseYMove),
            new InputData ("Mouse ScrollWheel", MouseScroll)
        };

        InputEvent.AddInput (inputDatas);
        InputEvent.AddInput (0, MouseDown, MouseType.Down);
        InputEvent.AddInput (0, MouseUp, MouseType.Up);
        InputEvent.AddInput (0, MpusePress, MouseType.Press);
        InputEvent.AddInput (2, ScrollDown, MouseType.Down);
        InputEvent.AddInput (2, ScrollUp, MouseType.Up);
        InputEvent.AddInput (1, RighMouseDown, MouseType.Down);
        InputEvent.AddInput (1, RightMouseUp, MouseType.Up);

        camera = GetComponentInChildren<Camera> ();
        myTransform = transform;
        cameraTransform = camera.transform;
    }

    private void RighMouseDown()
    {
        MouseRightPress = true;
    }

    private void RightMouseUp()
    {
        MouseRightPress = false;
    }

    private void OnDisable () {
        InputEvent.RemoveInput (inputDatas);
        InputEvent.RemoveInput (0, MouseDown, MouseType.Down);
        InputEvent.RemoveInput (0, MouseUp, MouseType.Up);
        InputEvent.RemoveInput (0, MpusePress, MouseType.Press);
        InputEvent.RemoveInput (2, ScrollDown, MouseType.Down);
        InputEvent.RemoveInput (2, ScrollUp, MouseType.Up);
    }
    private void MouseScroll (float axis) {
        Vector3 move = myTransform.position;
        move.y = Mathf.Clamp (move.y + axis * Speed, MinYPosition, MaxYPosition);
        myTransform.position = move;
    }

    private void MouseYMove (float axis) {
        if (!GameManager.Instatate.IsBuild)
        {
            if (MouseRightPress)
            {
                Vector3 rotate = cameraTransform.localEulerAngles;
                rotate.x = Mathf.Clamp(rotate.x + (VericalInver ? -axis : axis) * SpeedRotate, MinXRotate, MaxXRotate);
                cameraTransform.localEulerAngles = rotate;
            }
            if (ScrollPrees)
            {
                VerticalMove(myTransform.position.y / MaxYPosition * axis * -Speed);
            }
        }
    }

    private void MouseXMove (float axis) {
        if (!GameManager.Instatate.IsBuild)
        {
            if (MouseRightPress)
            {
                myTransform.Rotate(new Vector3(0, axis, 0) * SpeedRotate, Space.World);
            }
            if (ScrollPrees)
            {
                HorizontalMove(myTransform.position.y / MaxYPosition * axis * -Speed);
            }
        }
    }

    private void ScrollUp () {
        ScrollPrees = false;
    }

    private void ScrollDown () {
        ScrollPrees = true;
    }

    private void MpusePress () {
        PressEvent?.Invoke ();
    }

    private void MouseUp () {
        UpEvent?.Invoke ();
    }

    private void MouseDown () {
        DownEvent?.Invoke ();
    }

    private void VerticalMove (float axis) {
        myTransform.Translate (new Vector3 (0, 0, axis) * Time.deltaTime * Speed, Space.Self);
    }

    private void HorizontalMove (float axis) {
        myTransform.Translate (new Vector3 (axis, 0, 0) * Time.deltaTime * Speed, Space.Self);
    }
}