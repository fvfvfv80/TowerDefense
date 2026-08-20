using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerContext : IContext
    {
        private EnemyWaveDirector _enemyWaveDirector;


        public TowerContext(EnemyWaveDirector enemyWaveDirector)
        {
            _enemyWaveDirector = enemyWaveDirector;
        }

        public IEnumerable<BaseActor> FindTargetList()
        {
            return _enemyWaveDirector.CurrentWaveEnemyList;
        }

        public void Release()
        {

        }

    }
}
