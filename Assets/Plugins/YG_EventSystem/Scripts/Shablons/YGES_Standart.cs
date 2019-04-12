using UnityEngine;

namespace YG_EventSystem
{
    public class YGES_Standart : MonoBehaviour
    {
        protected InputData[] InputDatas = new InputData[0];
        protected EventData[] EventDatas = new EventData[0];
        protected virtual void Init()
        {
        }
        private void Awake()
        {
            Init();
        }
        private void OnEnable()
        {
            InputEvent.AddInput(InputDatas);
            GameEvent.AddEvent(EventDatas);
            EOnEnable();
        }
        private void OnDisable()
        {
            InputEvent.RemoveInput(InputDatas);
            GameEvent.RemoveEvent(EventDatas);
            EOnDisable();
        }

        public virtual void EOnEnable()
        {

        }
        public virtual void EOnDisable()
        {

        }
    }
}
