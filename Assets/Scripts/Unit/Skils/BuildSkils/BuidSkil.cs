using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem
{
    public class BuidSkil : YGES_Standart, ISkil
    {
        [SerializeField]
        private Sprite _icon;

        public Sprite Icon { get => _icon; set => _icon = value; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private EventData eventData;

        [SerializeField]
        private Transform _build;
        [SerializeField]
        private Transform _priBuild;

        [SerializeField]
        private float _offsetY;



        private Transform _buildActive;
        private RTS.ISelectebleObject _selecteble;

        protected override void Init()
        {
            _selecteble = GetComponent<RTS.ISelectebleObject>();
            eventData = new EventData(UpdateBuild, Method.Update);
        }

        private void UpdateBuild(float obj)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 9, QueryTriggerInteraction.Ignore))
            {
                _buildActive.position = new Vector3(hit.point.x - hit.point.x % .5f, hit.point.y, hit.point.z - hit.point.z % .5f) + Vector3.up * _offsetY;
            }
        }

        public void Active() {
            _buildActive = Instantiate(_priBuild);
            GameEvent.AddEvent(eventData);
            InputEvent.AddInput(0, Build, MouseType.Down);
            InputEvent.AddInput(1, End, MouseType.Down);
            Cursor.visible = false;
            GameManager.Instatate.IsBuild = true;
        }

        private void End()
        {
            Destroy(_buildActive.gameObject);
            GameEvent.RemoveEvent(eventData);
            InputEvent.RemoveInput(0, Build, MouseType.Down);
            InputEvent.RemoveInput(1, End, MouseType.Down);
            Cursor.visible = true;
            GameManager.Instatate.IsBuild = false;
        }

        private void Build()
        {
            Instantiate(_build, _buildActive.position, Quaternion.identity);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                End();
            }
        }
    }
}
