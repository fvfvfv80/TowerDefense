using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.Scenario;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{

    public class EnemyWaveDirectorContext : IContext
    {
        private readonly IFlowCreator _flowCreator;



        public EnemyWaveDirectorContext(IFlowCreator flowCreator )
        {
            _flowCreator = flowCreator;

        }


        public EnemyRoleFlow CreateEnemyRoleFlow()
        {
            return _flowCreator.CreateFlow<EnemyRoleFlow>();
        }


    }

    public class EnemyWaveDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveGameplay enemyWaveGameplay;

        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        private EnemyWaveScenario _enemyWaveScenario;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGameplay.CurrentWaveEnemyList;




        public void Init(EnemyWaveScenario enemyWaveScenario)
        {
            _enemyWaveScenario = enemyWaveScenario;
            enemyWaveGameplay.Init(this);
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


        public void RequestBindEnemy(EnemyActor enemyActor)
        {
            _enemyWaveScenario.BindEnemy(enemyActor);
        }


    }
}