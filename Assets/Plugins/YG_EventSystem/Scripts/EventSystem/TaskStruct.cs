using System;

namespace YG_EventSystem
{
    public class TaskStruct
    {
        public Action Action;
        private int Loop;
        private Action<float> UpdateAction;
        public TaskStruct(Action action, int loop)
        {
            Action = action;
            Loop = loop;
        }
        public TaskStruct(Action action, int loop, Action<float> updateAction)
        {
            Action = action;
            Loop = loop;
            UpdateAction = updateAction;
        }
        public bool Dicrimet(){
            bool result = Loop != 0;
            if(Loop > -1)
                Loop--;
            return result;
        }
        public int GetLoop => Loop;
        public bool IsMain => UpdateAction != null;
        public Action<float> GetUpdateAction => UpdateAction;
    }
}