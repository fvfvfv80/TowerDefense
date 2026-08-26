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
        private TowerBuffDirector towerBuffDirector;

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
            RegisterFlowCreation(CreateTowerBindFlow);

            RegisterFlowCreation(CreateTowerRoleFlow);
            RegisterFlowCreation(CreateEnemyRoleFlow);

        }

        private void InitDirectors()
        {

            var playerInputDirectorContext = new PlayerInputDirectorContext(this);
            playerInputDirector.Init(playerInputDirectorContext);

            var towerBuildDirectorContext = new TowerBuildDirectorContext(this);
            towerBuildDirector.Init(towerBuildDirectorContext);

            var towerBuffDirectorContext = new TowerBuffDirectorContext(this);
            towerBuffDirector.Init(towerBuffDirectorContext);

            var enemyWaveDirectorContext = new EnemyWaveDirectorContext(this);
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

        private TowerBindFlow CreateTowerBindFlow()
        {
            var flow = new TowerBindFlow(towerBuffDirector);
            return flow;
        }

        private TowerRoleFlow CreateTowerRoleFlow()
        {
            var flow = new TowerRoleFlow(enemyWaveDirector,towerBuffDirector);
            return flow;
        }

        private EnemyRoleFlow CreateEnemyRoleFlow()
        {
            var flow = new EnemyRoleFlow(enemyWaveDirector, player.HPModule, player.GoldModule);
            return flow;
        }

        #endregion

    }
}