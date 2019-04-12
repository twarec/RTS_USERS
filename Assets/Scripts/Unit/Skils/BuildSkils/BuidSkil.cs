using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem
{
    public class BuidSkil : YGES_Standart, ISkil
    {
        public Sprite Icon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private EventData eventData;

        [SerializeField]
        private Transform _build;
        [SerializeField]
        private Transform _priBuild;

        private Transform _buildActive;


        protected override void Init()
        {
            eventData = new EventData(UpdateBuild, Method.Update);
        }

        private void UpdateBuild(float obj)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 9, QueryTriggerInteraction.Ignore))
            {
                _buildActive.position = hit.point;
            }
        }

        public void Active() {
            _buildActive = Instantiate(_priBuild);
            GameEvent.AddEvent(eventData);
            InputEvent.AddInput(0, Build, MouseType.Up);
        }

        private void Build()
        {
            Instantiate(_build, _buildActive.position, Quaternion.identity);
            Destroy(_buildActive.gameObject);
            GameEvent.RemoveEvent(eventData);
            InputEvent.RemoveInput(0, Build, MouseType.Up);
        }
    }
}
