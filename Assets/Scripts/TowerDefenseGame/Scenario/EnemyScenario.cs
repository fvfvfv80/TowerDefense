using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Player;

namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class EnemyScenario : IEnemyScenario, IScenario
    {
        private EnemyWaveDirector _enemyWaveDirector;


        private PlayerHPModule _playerHP;


        private PlayerGoldModule _playerGold;


        public EnemyScenario(EnemyWaveDirector enemyWaveDirector, PlayerHPModule playerHPModule, PlayerGoldModule playerGoldModule)
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
            int gold = enemyActor.RewardGold;
            DropGold(gold);

            _enemyWaveDirector.DespawnWaveEnemy(enemyActor);

        }

        public void NotifyReachedGoal(EnemyActor enemyActor)
        {
            _playerHP.TakeDamage(1);

            _enemyWaveDirector.DespawnWaveEnemy(enemyActor);

        }


    }
}
