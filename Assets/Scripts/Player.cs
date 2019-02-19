using System;
using UnityEngine;
using YG_EventSystem;
using System.Linq;

public class Player : MonoBehaviour {
    [SerializeField]
    private LayerMask NavigationMask;
    [SerializeField]
    private Camera camera;
    private void Awake () {
        RTS.SelectebleObjectManager.ActionAddSelectebleObject = v => v.IsSelect = true;
        RTS.SelectebleObjectManager.ActionRemoveSelectebleObject = v => v.IsSelect = false;
        InputEvent.AddInput(1, MouseUp, MouseType.Up);
    }

    private void MouseUp()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, NavigationMask))
        {
            Vector3 point = hit.point;
            var coll = RTS.SelectebleObjectManager.GetSelectionObjects();
            int sqrt = (int)Mathf.Sqrt(coll.Count);
            float row = 0, col = 0;
            bool isleft = false;

            
            foreach (var v in RTS.SelectebleObjectManager.GetAllSelctioObjects())
            {
                if (v.IsSelect)
                {
                    v.Move(point - (Vector3.forward * col + Vector3.right * (isleft ? -row : row)) * 2f);
                    isleft = !isleft;
                    if(isleft)
                        row++;
                    if (row * 2f >= sqrt)
                    {
                        row = 0;
                        isleft = false;
                        col++;
                    }
                }
            }

        }
    }
}