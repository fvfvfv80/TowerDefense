using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Tower;
using System.Collections.Generic;


namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class TowerScenario : ITowerScenario, IScenario
    {
        private EnemyWaveDirector _enemyWaveDirector;

        private TowerBuffDirector _towerBuffDirector;


        public TowerScenario(EnemyWaveDirector enemyWaveDirector, TowerBuffDirector towerBuffDirector)
        {
            _enemyWaveDirector = enemyWaveDirector;

            _towerBuffDirector = towerBuffDirector;
        }

        public IEnumerable<BaseActor> FindAttackTargetList()
        {
            return _enemyWaveDirector.CurrentWaveEnemyList;
        }

        public IEnumerable<BaseActor> FindBuffTargetList()
        {
            return _towerBuffDirector.CurrentTowers;
        }
    }
}
