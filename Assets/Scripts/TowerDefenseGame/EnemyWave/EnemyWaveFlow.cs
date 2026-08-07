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

    public interface IEnemyWaveFlowHost
    {
        public EnemyActor SpawnEnemy(GameObject enemyPrefab);


    }

    public class EnemyWaveFlow : MonoBehaviour
    {
        [SerializeField]
        private Wave[] waves;

        [SerializeField]
        private IEnemyWaveFlowHost enemyWaveFlowHost;

        private int _currentWaveIndex = -1;

        private Wave _currentWave;

        private List<EnemyActor> _currentWaveEnemyList = new();

        public List<EnemyActor> CurrentWaveEnemyList => _currentWaveEnemyList;

        public int CurrentEnemyCount => _currentWaveEnemyList.Count;

        public int MaxWaveEnemyCount => _currentWave.maxEnemyCount;

        public int CurrentWaveCount => _currentWaveIndex + 1;

        public int MaxWave => waves.Length;


        public void BindFlowHost(IEnemyWaveFlowHost flowHost)
        {
            enemyWaveFlowHost = flowHost;
        }

        public void TryStartWave()
        {
            if (_currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;

                StartWave(waves[_currentWaveIndex]);
            }
        }

        public void StartWave(Wave wave)
        {
            _currentWave = wave;

            StartCoroutine(nameof(SpawnEnemy));
        }

        private IEnumerator SpawnEnemy()
        {
            int spawnEnemyCount = 0;

            while (CurrentEnemyCount == 0 && spawnEnemyCount < _currentWave.maxEnemyCount)
            {
                int enemyIndex = Random.Range(0, _currentWave.enemyPrefabs.Length);

                var enemyActor = enemyWaveFlowHost.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);
                //enemySpawner.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);

                _currentWaveEnemyList.Add(enemyActor);

                spawnEnemyCount++;

                yield return new WaitForSeconds(_currentWave.spawnTime);
            }
        }


        public void DespawnEnemy(EnemyActor enemyActor)
        {
            _currentWaveEnemyList.Remove(enemyActor);
        }
    }
}