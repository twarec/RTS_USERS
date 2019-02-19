using UnityEngine;
using UnityEngine.AI;

namespace RTS {
    public interface ISelectebleObject {
        string Name { get; set; }
        int Tag { get; set; }
        bool IsSelect { get; set; }
        Transform Transform { get;}
        NavMeshAgent NavMeshAgent { get; }
        void Move(Vector3 pos);
        Texture2D Icon { get; }
        float Health { get; set; }
        Vector3 Position { get; set; }
    }
}