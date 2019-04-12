using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem
{
    public class UpdateShablon : YGES_Standart
    {
        protected override void Init()
        {
            EventDatas = new EventData[]
            {
                new EventData(EUpdate, Method.Update)
            };
        }

        private void EUpdate(float obj)
        {
            
        }
    }
}
