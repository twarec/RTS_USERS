using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    [AddComponentMenu("RTS/SelectebleObject/Builder/Test")]
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class BuilderTest : MonoBehaviour, ISelectebleObject
    {
        #region Переменные испектора
        [SerializeField]
        private string _name;
        [SerializeField]
        private int _tag;
        [SerializeField]
        private bool _isSelect;
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private float _health;
        [SerializeField]
        private Vector3 _position;
        #endregion
        #region Интерфейс ISelectebleObject
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public int Tag
        {
            get => _tag;
            set => _tag = value;
        }
        public bool IsSelect
        {
            get => _isSelect;
            set
            {
                _isSelect = value;
                if (_outline)
                {
                    _outline.enabled = _isSelect;
                }
            }
        }
        public Transform Transform
        {
            get
            {
                return _transform;
            }
        }
        public NavMeshAgent NavMeshAgent
        {
            get
            {
                return _navMeshAgent;
            }
        }
        public Sprite Icon
        {
            get => _icon;
        }
        public float Health
        {
            get => _health;
            set => _health = value;
        }
        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }
        public Skils Skils { get => _skils; }

        public void Move(Vector3 pos)
        {
            _navMeshAgent.destination = pos;
        }
        #endregion 

        private Outline _outline;
        private Skils _skils;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _transform = transform;
            _skils = GetComponent<Skils>();
        }
        private void Start()
        {
            _outline.enabled = false;
        }
        private void OnEnable()
        {
            SelectebleObjectManager.RegistrateSelectionObject(this);
        }
        private void OnDisable()
        {
            SelectebleObjectManager.DiRegistrateSelectionObject(this);
        }
    }
}
