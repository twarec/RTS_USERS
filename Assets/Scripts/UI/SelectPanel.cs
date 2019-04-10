using System;
using System.Collections;
using System.Collections.Generic;
using RTS;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField]
    private SelectPanelEllement[] SelectElllements = new SelectPanelEllement[0];


    private void Awake()
    {
        SelectElllements = GetComponentsInChildren<SelectPanelEllement>();

        RTS.SelectebleObjectManager.ActionAddSelectebleObject += SelectebleUnit;
        RTS.SelectebleObjectManager.ActionRemoveSelectebleObject += RemoveUnit;
    }

    private void RemoveUnit(ISelectebleObject obj)
    {
        foreach(var v in SelectElllements)
        {
            if(v.RemoveUnit(obj))
            {
                break;
            }
        }
    }

    private void SelectebleUnit(ISelectebleObject obj)
    {
        foreach(var v in SelectElllements)
        {
            if(v.AddUnit(obj))
            {
                break;
            }
        }
    }
}
