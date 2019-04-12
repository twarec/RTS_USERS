namespace YG_EventSystem
{
    public struct EventData
    {
        public System.Action<float> action;
        public Method method;
        public EventData(System.Action<float> action, Method method)
        {
            this.action = action;
            this.method = method;
        }
    }
}
