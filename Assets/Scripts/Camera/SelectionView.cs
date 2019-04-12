using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG_EventSystem;

public class SelectionView : MonoBehaviour
{

    [SerializeField]
    private LayerMask _selectLayers;

    private RTS_Camera RTS_Camera;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Player player;
    private Vector2 Point1, Point2;
    private Vector3 WPoint1;


    private static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (!_whiteTexture)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }
            return _whiteTexture;
        }
    }
    public static Action<List<RTS.ISelectebleObject>> SelectAction;


    private static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }
    public static void DrawScreenRectBorder(Rect rect, Color color, float thickness)
    {
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        DrawScreenRect(rect, new Color(.8f, .8f, .95f, .25f));
    }
    public static Bounds GetViewportBounds(Camera camera, Vector3 p1, Vector3 p2)
    {
        var v1 = camera.ScreenToViewportPoint(p1);
        var v2 = camera.ScreenToViewportPoint(p2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;
        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }


    private void Awake()
    {
        RTS_Camera = GetComponent<RTS_Camera>();
        if (player == null)
            player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        RTS_Camera.UpEvent.AddListener(MouseUp);
        RTS_Camera.DownEvent.AddListener(MouseDown);
        RTS_Camera.PressEvent.AddListener(MousePrees);
    }
    private void OnDisable()
    {
        RTS_Camera.UpEvent.RemoveListener(MouseUp);
        RTS_Camera.DownEvent.RemoveListener(MouseDown);
        RTS_Camera.PressEvent.RemoveListener(MousePrees);
    }

    public void MouseDown()
    {
        if (!GameManager.Instatate.IsBuild)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                isDraw = true;
                GameEvent.AddEvent(SelectionViewUpdate, Method.Update);
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    WPoint1 = hit.point;
                    Point1 = camera.WorldToScreenPoint(WPoint1);
                }
            }
        }
    }
    public void MousePrees()
    {
        Point1 = camera.WorldToScreenPoint(WPoint1);
        Point2 = Input.mousePosition;
    }
    public void MouseUp()
    {
        isDraw = false;
        GameEvent.RemoveEvent(SelectionViewUpdate, Method.Update);
        SelectAction?.Invoke(RTS.SelectebleObjectManager.GetSelectionObjects());
    }

    private void SelectionViewUpdate(float obj)
    {
        if (Vector3.SqrMagnitude(Point1 - Point2) > 1)
        {
            var bounds = GetViewportBounds(camera, Point1, Point2);
            foreach (var v in RTS.SelectebleObjectManager.GetAllSelctioObjects())
            {
                if (bounds.Contains(camera.WorldToViewportPoint(v.Transform.position)))
                {
                    if (v.Tag == player.Tag)
                    {
                        if (!v.IsSelect)
                        {
                            RTS.SelectebleObjectManager.AddSelectonObject(v);
                        }
                    }
                }
                else
                {
                    if (v.Tag == player.Tag)
                    {
                        if (v.IsSelect)
                        {
                            RTS.SelectebleObjectManager.RemoveSelectonObject(v);
                        }
                    }
                }
            }
        }
        else
        {
            List<RTS.ISelectebleObject> DiSil = new List<RTS.ISelectebleObject>();
            foreach (var v in RTS.SelectebleObjectManager.GetSelectionObjects())
                DiSil.Add(v);
            foreach (var v in DiSil)
                RTS.SelectebleObjectManager.RemoveSelectonObject(v);
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Point2), out hit, 1000, _selectLayers))
            {
                RTS.ISelectebleObject selecteble = hit.transform.GetComponent<RTS.ISelectebleObject>();
                if (selecteble != null)
                {
                    RTS.SelectebleObjectManager.AddSelectonObject(selecteble);
                }
            }
        }
    }


    private bool isDraw = false;
    private void OnGUI()
    {
        if (isDraw)
        {
            var p1 = Point1;
            var p2 = Point2;
            p1.y = Screen.height - p1.y;
            p2.y = Screen.height - p2.y;

            var min = Vector3.Min(p1, p2);
            var max = Vector3.Max(p1, p2);
            Rect rect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
            DrawScreenRectBorder(rect, new Color(.8f, .8f, .95f), 1);
        }
    }

}
