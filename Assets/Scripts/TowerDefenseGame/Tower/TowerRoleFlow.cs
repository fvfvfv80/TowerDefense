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

        private TowerBuffDirector _towerBuffDirector;


        public TowerRoleFlow(EnemyWaveDirector enemyWaveDirector, TowerBuffDirector towerBuffDirector)
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
