using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilsPanel : MonoBehaviour
{
    [SerializeField]
    private SkilsPanaleEllement[] SelectElllements = new SkilsPanaleEllement[0];

    private void Awake()
    {
        SelectElllements = GetComponentsInChildren<SkilsPanaleEllement>();
    }

    public void LoadSkilsFromUnit(RTS.ISelectebleObject selecteble)
    {
        for (int i = 0; i < SelectElllements.Length; i++)
        {
            if (i < selecteble.Skils.AllSkils.Length)
            {
                SelectElllements[i].Icon = selecteble.Skils.AllSkils[i].Icon;
                SelectElllements[i].Action = selecteble.Skils.AllSkils[i].Active;
                SelectElllements[i].gameObject.SetActive(true);
            }
            else
            {
                SelectElllements[i].gameObject.SetActive(false);
            }
        }
    }
}
