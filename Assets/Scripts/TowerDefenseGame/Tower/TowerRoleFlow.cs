using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerRoleFlow : IFlow
    {
        private EnemyWaveDirector _enemyWaveDirector;


        public TowerRoleFlow(EnemyWaveDirector enemyWaveDirector)
        {
            _enemyWaveDirector = enemyWaveDirector;
        }

        public IEnumerable<BaseActor> FindTargetList()
        {
            return _enemyWaveDirector.CurrentWaveEnemyList;
        }

        public void EndFlow()
        {

        }

    }
}
