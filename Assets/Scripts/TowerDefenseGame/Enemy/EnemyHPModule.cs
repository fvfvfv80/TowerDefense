using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyHPModule : MonoBehaviour
    {
        [SerializeField]
        private float maxHP;

        private float _currentHP;
        private bool _isDie = false;

        private IEnemyModuleHost _enemyModuleHost;

        public Action<EnemyHPModule> OnHpChanged;

        public float MaxHP => maxHP;
        public float CurrentHP => _currentHP;

        

        public void Init(IEnemyModuleHost enemyEventHandler)
        {
            _enemyModuleHost = enemyEventHandler;
        }

        public void Setup()
        {
            _currentHP = maxHP;
            _isDie = false;
        }


        public void TakeDamage(float damage)
        {
            if (_isDie)
                return;

            _currentHP -= damage;

            _enemyModuleHost.RequestHit();

            if (_currentHP <= 0)
            {
                _isDie = true;
                _enemyModuleHost.RequestReachGoal(EnemyDestroyType.Kill);
            }
            OnHpChanged?.Invoke(this);
        }


    }
}
