using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS
{
    [AddComponentMenu("RTS/SelectebleObject/Manager")]
    public class SelectebleObjectManager : MonoBehaviour
    {

        private static SelectebleObjectManager _instatate;
        public static SelectebleObjectManager Instatate
        {
            get
            {
                if (!_instatate)
                {
                    GameObject go = new GameObject("SelectebleObjectManager");
                    _instatate = go.AddComponent<SelectebleObjectManager>();
                }
                return _instatate;
            }
        }

        public static Action<ISelectebleObject>
            ActionAddSelectebleObject,
            ActionRemoveSelectebleObject;
        [SerializeField]
        private List<ISelectebleObject> _selectionObjects = new List<ISelectebleObject>();
        public List<ISelectebleObject> SelectebleObjects => _selectionObjects;

        private List<ISelectebleObject> _allSelectionObjects = new List<ISelectebleObject>();
        public List<ISelectebleObject> AllSelctioObjects => _allSelectionObjects;

        public static List<ISelectebleObject> GetAllSelctioObjects() => Instatate.AllSelctioObjects;
        public static List<ISelectebleObject> GetSelectionObjects() => Instatate.SelectebleObjects;

        public static void RegistrateSelectionObject(ISelectebleObject value)
        {
            Instatate._allSelectionObjects.Add(value);
        }
        public static void DiRegistrateSelectionObject(ISelectebleObject value)
        {
            _instatate?._allSelectionObjects.Remove(value);
        }

        public static void AddSelectonObject(ISelectebleObject value)
        {
            Instatate._selectionObjects.Add(value);
            ActionAddSelectebleObject(value);
        }
        public static void RemoveSelectonObject(ISelectebleObject value)
        {
            if (_instatate)
            {
                _instatate._selectionObjects.Remove(value);
                ActionRemoveSelectebleObject(value);
            }
        }
    }
}