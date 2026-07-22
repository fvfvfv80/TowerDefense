using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{
    public class EnemyWaveFlow : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnFlow enemySpawner;

        private Wave _currentWave;

        private int _currentEnemyCount;

        public int CurrentEnemyCount => _currentEnemyCount;

        public int MaxEnemyCount => _currentWave.maxEnemyCount;


        private void Awake()
        {

        }

        public void StartWave(Wave wave)
        {
            _currentWave = wave;

            _currentEnemyCount = _currentWave.maxEnemyCount;

            StartCoroutine(nameof(SpawnEnemy));
        }

        private IEnumerator SpawnEnemy()
        {
            int spawnEnemyCount = 0;


            while (spawnEnemyCount < _currentWave.maxEnemyCount)
            {
                int enemyIndex = Random.Range(0, _currentWave.enemyPrefabs.Length);

                var enemyActor = enemySpawner.SpawnEnemy(_currentWave.enemyPrefabs[enemyIndex]);

                spawnEnemyCount++;

                yield return new WaitForSeconds(_currentWave.spawnTime);
            }
        }

       
        public void ReduceEnemyCount()
        {
            _currentEnemyCount--;
        }
    }
}