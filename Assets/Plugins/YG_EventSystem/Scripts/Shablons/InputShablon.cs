using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem
{
    public class InputShablon : YGES_Standart
    {
        protected override void Init()
        {
            InputDatas = new InputData[] {
                new InputData("Mouse X", InputMouseX)
            };
        }

        private void InputMouseX(float obj)
        {
            
        }
    }
}
