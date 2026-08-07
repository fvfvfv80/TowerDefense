using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class TowerDefenseEnemyRolePlay : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveDirector enemyWaveDirector;

        [SerializeField]
        private PlayerHPModule _playerHP;

        [SerializeField]
        private PlayerGoldModule _playerGold;


        public void OrderDropGold(int gold)
        {
            _playerGold.CurrentGold += gold;
        }

        public void OrderAttackPlayer()
        {
            _playerHP.TakeDamage(1);
        }

        public void OrderDespawnEnemy(EnemyActor enemyActor)
        {
            enemyWaveDirector.DespawnEnemy(enemyActor);
        }
    }
}