using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
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

        private EnemyWaveDirectorContext _context;

        public List<EnemyActor> CurrentWaveEnemyList => enemyWaveGameplay.CurrentWaveEnemyList;




        public void Init(EnemyWaveDirectorContext enemyWaveDirectorContext)
        {
            _context = enemyWaveDirectorContext;
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


        public EnemyRoleFlow CreateEnemyRoleFlow()
        {
            return _context.CreateEnemyRoleFlow();
        }
    }
}