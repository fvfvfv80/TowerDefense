using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{


    public class EnemyWaveDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveGamePlay enemyWaveGamePlay;

        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        private IFlowCreator _flowCreator;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGamePlay.CurrentWaveEnemyList;



        public void Init(IFlowCreator flowCreator)
        {
            _flowCreator = flowCreator;

            enemyWaveGamePlay.Init(_flowCreator);
        }

        


        public void OnEnemyWaveStartButtonClick()
        {
            HandleWaveStart();
        }

        private void HandleWaveStart()
        {
            enemyWaveGamePlay.StartWave();
        }


        public void DespawnWaveEnemy(EnemyActor enemyActor)
        {
            enemyWaveGamePlay.RemoveWaveEnemy(enemyActor);

            enemySpawnModule.DespawnEnemy(enemyActor);

        }
    }
}