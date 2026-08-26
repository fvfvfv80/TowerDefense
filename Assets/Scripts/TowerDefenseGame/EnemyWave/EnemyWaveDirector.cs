using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.Scenario;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{

    public interface IEnemyWaveScenario
    {
        void NotifyEnemySpawned(EnemyActor enemyActor);
    }


    public class EnemyWaveDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveGameplay enemyWaveGameplay;

        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        private IEnemyWaveScenario _enemyWaveScenario;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGameplay.CurrentWaveEnemyList;




        public void Init(IEnemyWaveScenario enemyWaveScenario)
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


        public void NotifyEnemySpawned(EnemyActor enemyActor)
        {
            _enemyWaveScenario.NotifyEnemySpawned(enemyActor);
        }


    }
}