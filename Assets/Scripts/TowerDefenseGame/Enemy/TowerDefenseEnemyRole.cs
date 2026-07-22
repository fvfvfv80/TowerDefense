using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class TowerDefenseEnemyRole : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnFlow enemySpawner;

        [SerializeField]
        private EnemyWaveFlow enemyWave;

        [SerializeField]
        private PlayerHPModule _playerHP;

        [SerializeField]
        private PlayerGoldModule _playerGold;


        public void DropGold(int gold)
        {
            _playerGold.CurrentGold += gold;
        }

        public void AttackPlayer()
        {
            _playerHP.TakeDamage(1);
        }

        public void DespawnEnemy(EnemyActor enemyActor)
        {
            enemySpawner.DespawnEnemy(enemyActor);
            enemyWave.ReduceEnemyCount();
        }
    }
}