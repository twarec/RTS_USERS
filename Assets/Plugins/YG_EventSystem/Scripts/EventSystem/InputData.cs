using System;

namespace YG_EventSystem
{
    public struct InputData
    {
        public string Name;
        public Action<float> Action;
        public InputData(string name, Action<float> action){
            Name = name;
            Action = action;
        }
    }
}
