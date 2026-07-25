using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyHPModule : MonoBehaviour
    {
        [SerializeField]
        private float maxHP;

        [SerializeField]
        private EnemyBaseModule enemyBase;

        private float _currentHP;
        private bool _isDie = false;

        public float MaxHP => maxHP;
        public float CurrentHP => _currentHP;

        private IEnemyHandler _enemyEventHandler;

        public void Init(IEnemyHandler enemyEventHandler)
        {
            _enemyEventHandler = enemyEventHandler;
        }

        public void Setup()
        {
            _currentHP = maxHP;
        }


        public void TakeDamage(float damage)
        {
            if (_isDie)
                return;

            _currentHP -= damage;

            _enemyEventHandler.Hit();

            if (_currentHP <= 0)
            {
                _isDie = true;
                _enemyEventHandler.Despawn(EnemyDestroyType.Kill);
            }
        }


    }
}
