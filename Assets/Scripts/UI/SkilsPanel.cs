using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilsPanel : MonoBehaviour
{
    [SerializeField]
    private SelectPanelEllement[] SelectElllements = new SelectPanelEllement[0];

    private void Awake()
    {
        SelectElllements = GetComponentsInChildren<SelectPanelEllement>();
    }

    private void LoadSkilsFromUnit(RTS.ISelectebleObject selecteble)
    {
        foreach(var v in selecteble.Skils.AllSkils)
        {

        }
    }
}
