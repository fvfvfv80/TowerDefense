using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyRoleFlow : IFlow
    {

        private EnemyWaveDirector _enemyWaveDirector;


        private PlayerHPModule _playerHP;


        private PlayerGoldModule _playerGold;


        public EnemyRoleFlow(EnemyWaveDirector enemyWaveDirector, PlayerHPModule playerHPModule, PlayerGoldModule playerGoldModule)
        {
            _enemyWaveDirector = enemyWaveDirector;
            _playerHP = playerHPModule;
            _playerGold = playerGoldModule;
        }

        private void DropGold(int gold)
        {
            _playerGold.CurrentGold += gold;
        }

        public void NotifyKilled(EnemyActor enemyActor)
        {
            int gold = enemyActor.EnemyBase.RewardGold;
            DropGold(gold);

            _enemyWaveDirector.DespawnWaveEnemy(enemyActor);

        }

        public void NotifyReachGoal(EnemyActor enemyActor)
        {
            _playerHP.TakeDamage(1);

            _enemyWaveDirector.DespawnWaveEnemy(enemyActor);
  
        }

        public void EndFlow(EnemyActor enemyActor)
        {
            
           
        }




    }
}