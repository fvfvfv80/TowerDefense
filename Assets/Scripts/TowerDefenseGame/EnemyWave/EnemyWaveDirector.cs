using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{


    public class EnemyWaveDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveGameplay enemyWaveGameplay;

        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        private IFlowCreator _flowCreator;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGameplay.CurrentWaveEnemyList;



        public void Init(IFlowCreator flowCreator)
        {
            _flowCreator = flowCreator;

            enemyWaveGameplay.Init(_flowCreator);
        }

        


        public void OnEnemyWaveStartButtonClick()
        {
            HandleWaveStart();
        }

        private void HandleWaveStart()
        {
            enemyWaveGameplay.StartWave();
        }


        public void DespawnWaveEnemy(EnemyActor enemyActor)
        {
            enemyWaveGameplay.RemoveWaveEnemy(enemyActor);

            enemySpawnModule.DespawnEnemy(enemyActor);

        }
    }
}