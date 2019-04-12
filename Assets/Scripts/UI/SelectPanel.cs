using System;
using System.Collections;
using System.Collections.Generic;
using RTS;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField]
    private SelectPanelEllement[] SelectElllements = new SelectPanelEllement[0];


    [SerializeField]
    private SkilsPanel _skilsPanel;

    private int _count;


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
                _count--;
                break;
            }
        }
        if (_count != 1)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                _skilsPanel.gameObject.SetActive(false);
            }
        }
        else
        {
            if (RTS.SelectebleObjectManager.GetSelectionObjects().Count > 0 && RTS.SelectebleObjectManager.GetSelectionObjects()[0].Skils != null)
            {
                gameObject.SetActive(false);
                _skilsPanel.gameObject.SetActive(true);
                _skilsPanel.LoadSkilsFromUnit(RTS.SelectebleObjectManager.GetSelectionObjects()[0]);
            }
        }
    }

    private void SelectebleUnit(ISelectebleObject obj)
    {
        foreach(var v in SelectElllements)
        {
            if(v.AddUnit(obj))
            {
                _count++;
                break;
            }
        }
        if(_count != 1)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                _skilsPanel.gameObject.SetActive(false);
            }
        }
        else
        {
            if (RTS.SelectebleObjectManager.GetSelectionObjects()[0].Skils != null)
            {
                gameObject.SetActive(false);
                _skilsPanel.gameObject.SetActive(true);
                _skilsPanel.LoadSkilsFromUnit(RTS.SelectebleObjectManager.GetSelectionObjects()[0]);

            }
        }
    }
}
