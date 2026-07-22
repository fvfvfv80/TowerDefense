using Assets.Scripts.TowerDefenseGame.Enemy;
using System;
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


    public class EnemyWaveSequenceFlow : MonoBehaviour
    {
        [SerializeField]
        private Wave[] waves;

        [SerializeField]
        private EnemySystem enemySystem;

        [SerializeField]
        private EnemyWaveFlow enemySpawnFlow;


        private int _currentWaveIndex = -1;


        public int CurrentWaveCount => _currentWaveIndex + 1;
        public int MaxWave => waves.Length;



        public void StartWave()
        {
            if (enemySystem.EnemyList.Count == 0 && _currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;

                enemySpawnFlow.StartWave(waves[_currentWaveIndex]);
            }
        }

    }
}