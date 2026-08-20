using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{

    public class EnemyWaveDirectorContext : IContext
    {
        private readonly IFlowCreator _flowCreator;
        private readonly IContextCreator _contextCreator;


        public EnemyWaveDirectorContext(IFlowCreator flowCreator, IContextCreator contextCreator)
        {
            _flowCreator = flowCreator;
            _contextCreator = contextCreator;
        }


        public EnemyContext CreateEnemyContext()
        {
            return _contextCreator.CreateContext<EnemyContext>();
        }


    }

    public class EnemyWaveDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveGameplay enemyWaveGameplay;

        [SerializeField]
        private EnemySpawnModule enemySpawnModule;

        private EnemyWaveDirectorContext _enemyWaveHostContext;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGameplay.CurrentWaveEnemyList;




        public void Init(EnemyWaveDirectorContext enemyWaveHostContext)
        {
            _enemyWaveHostContext = enemyWaveHostContext;
            enemyWaveGameplay.Init(_enemyWaveHostContext);
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