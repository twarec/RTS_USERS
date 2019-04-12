using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YG_EventSystem;

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
        private Skils _skils;
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
        private Material _myMaterial;
        private SphereCollider _triggerCgildren;

        private void Awake()
        {
            _skils = GetComponent<Skils>();
            _outline = GetComponent<Outline>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _transform = transform;
            _skils = GetComponent<Skils>();
            _myMaterial = GetComponent<MeshRenderer>().material;

            for(int i = 0; i < GameManager.Instatate.Players.Count; i++)
                if(GameManager.Instatate.Players[i].Tag == Tag)
                    _myMaterial.SetColor("_Color", GameManager.Instatate.Players[i].ColorPlayer);
            _triggerCgildren = new GameObject("Trigger").AddComponent<SphereCollider>();
            _triggerCgildren.isTrigger = true;
            _triggerCgildren.radius = 10;
            _triggerCgildren.transform.parent = _transform;
            _triggerCgildren.transform.localPosition = Vector3.zero;
            _triggerCgildren.gameObject.layer = LayerMask.NameToLayer("DatactedEnum");

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
