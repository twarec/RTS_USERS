using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class Skils : MonoBehaviour
    {
        private ISkil[] _skils;
        public ISkil[] AllSkils { get => _skils; }
        private void Awake()
        {
            _skils = GetComponents<ISkil>();
        }

    }
}
