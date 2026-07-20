using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyBaseModule : MonoBehaviour
    {
        [SerializeField]
        private int gold = 10;

        public int Gold => gold;

        public Action OnDie;
        public void KillEnemy()
        {
            OnDie?.Invoke();
        }
        
    }
}