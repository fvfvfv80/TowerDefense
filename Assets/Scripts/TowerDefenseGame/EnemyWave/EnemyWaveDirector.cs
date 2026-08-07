using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{

    public class WaveInfo
    {
        public int WaveEnemyCount;
    }

    public class EnemyWaveDirector : MonoBehaviour , IEnemyWaveFlowHost
    {
        [SerializeField]
        private EnemySpawnFlow enemySpawnFlow;

        [SerializeField]
        private EnemyWaveFlow enemyWaveFlow;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveFlow.CurrentWaveEnemyList;


        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            enemyWaveFlow.BindFlowHost(this);
        }

        public void OrderWaveStart()
        {

            enemyWaveFlow.TryStartWave();
          
        }

        public EnemyActor SpawnEnemy(GameObject enemyPrefab)
        {
            var enemyActor = enemySpawnFlow.SpawnEnemy(enemyPrefab);
            return enemyActor;
        }


        public void DespawnEnemy(EnemyActor enemyActor)
        {
            enemyWaveFlow.DespawnEnemy(enemyActor);
        }
    }
}