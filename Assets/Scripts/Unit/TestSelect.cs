using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    [AddComponentMenu("RTS/SelectebleObject/Test")]
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestSelect : MonoBehaviour, ISelectebleObject
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
        private Texture2D _icon;
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
                if (outline)
                {
                    outline.enabled = _isSelect;
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
        public Texture2D Icon
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

        public void Move(Vector3 pos)
        {
            _navMeshAgent.destination = pos;
        }
        #endregion


        private Outline outline;

        private void Awake()
        {
            outline = GetComponent<Outline>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _transform = transform;
        }
        private void Start()
        {
            outline.enabled = false;
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