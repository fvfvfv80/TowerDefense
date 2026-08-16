using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{

    [System.Serializable]
    public struct Wave
    {
        public float spawnTime;
        public int maxEnemyCount;
        public GameObject[] enemyPrefabs;

    }


    public class EnemyWaveGamePlay : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        [SerializeField]
        private Wave[] waves;

        [SerializeField]
        private Transform[] wayPoints; //시스템으로 갈수도있음


        private IFlowCreator _flowCreater;

        private int _currentWaveIndex = -1;

        private Wave _currentWave;

        private List<EnemyActor> _currentWaveEnemyList = new();

        public List<EnemyActor> CurrentWaveEnemyList => _currentWaveEnemyList;

        public int CurrentEnemyCount => _currentWaveEnemyList.Count;

        public int MaxWaveEnemyCount => _currentWave.maxEnemyCount;

        public int CurrentWaveCount => _currentWaveIndex + 1;

        public int MaxWave => waves.Length;


        public void Init(IFlowCreator flowCreator)
        {
            _flowCreater = flowCreator;
        }

        public void StartWave()
        {
            if (CurrentEnemyCount == 0 && _currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;

                _currentWave = waves[_currentWaveIndex];

                StartCoroutine(nameof(SpawnEnemy));
            }
        }


        private IEnumerator SpawnEnemy()
        {
            int spawnEnemyCount = 0;

            while (spawnEnemyCount < _currentWave.maxEnemyCount)
            {
                int enemyIndex = Random.Range(0, _currentWave.enemyPrefabs.Length);

                var enemyFlow = _flowCreater.CreateFlow<EnemyRoleFlow>();

                var enemyActor = enemySpawnModule.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);
                //enemyWaveFlowHost.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);
                //enemySpawner.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);

                enemyActor.SetupRoleFlow(enemyFlow);
                enemyActor.SetupPath(wayPoints);
                enemyActor.StartEnemy();

                _currentWaveEnemyList.Add(enemyActor);

                spawnEnemyCount++;

                yield return new WaitForSeconds(_currentWave.spawnTime);
            }
        }


        public void RemoveWaveEnemy(EnemyActor enemyActor)
        {
            _currentWaveEnemyList.Remove(enemyActor);
        }
    }
}