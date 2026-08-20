using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.SceneRoot
{
    public class TowerDefenseGameSceneRoot : BaseSceneRoot
    {
        [SerializeField]
        private PlayerInputDirector playerInputDirector;

        [SerializeField]
        private TowerBuildDirector towerBuildDirector;

        [SerializeField]
        private EnemyWaveDirector enemyWaveDirector;

        [SerializeField]
        private PlayerActor player;


        private void Awake()
        {
            RegisterCreations();

            InitDirectors();
        }

        private void RegisterCreations()
        {

            RegisterFlowCreation(CreatePlayerTowerBuildFlow);
            RegisterFlowCreation(CreateSelectedTowerMaintenanceFlow);

            RegisterContextCreation(CreateTowerContext);
            RegisterContextCreation(CreateEnemyContext);

        }

        private void InitDirectors()
        {

            var playerInputDirectorContext = new PlayerInputDirectorContext(this,this);
            playerInputDirector.Init(playerInputDirectorContext);

            var towerBuildDirectorContext = new TowerBuildDirectorContext(this, this);
            towerBuildDirector.Init(towerBuildDirectorContext);

            var enemyWaveDirectorContext = new EnemyWaveDirectorContext(this, this);
            enemyWaveDirector.Init(enemyWaveDirectorContext);
        }

        #region FlowFactory

        private PlayerTowerBuildFlow CreatePlayerTowerBuildFlow()
        {
            var flow = new PlayerTowerBuildFlow(towerBuildDirector, player.GoldModule);

            return flow;
        }
        private SelectedTowerMaintenanceFlow CreateSelectedTowerMaintenanceFlow()
        {
            var flow = new SelectedTowerMaintenanceFlow(towerBuildDirector, player.GoldModule);
            return flow;
        }



        #endregion

        #region ContextFactory

        private TowerContext CreateTowerContext()
        {
            var context = new TowerContext(enemyWaveDirector);
            return context;
        }

        private EnemyContext CreateEnemyContext()
        {
            var context = new EnemyContext(enemyWaveDirector, player.HPModule, player.GoldModule);
            return context;
        }

        #endregion
    }
}