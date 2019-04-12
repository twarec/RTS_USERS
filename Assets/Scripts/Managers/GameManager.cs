using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace YG_EventSystem
{
    public class GameManager : YGES_Standart
    {
        private static GameManager _instatate;
        private List<Player> _players;



        public static GameManager Instatate
        {
            get
            {
                if (!_instatate)
                {
                    _instatate = new GameObject("GameManager").AddComponent<GameManager>();
                }
                return _instatate;
            }
        }
        public List<Player> Players { get => _players; }


        public bool IsBuild;
        protected override void Init()
        {
            if (!_instatate)
            {
                _players = FindObjectsOfType<Player>().ToList();
                
                
                _instatate = this;
            }
        }
    }
}
