using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
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
            RegisterFlows();

            InitDirectors();
        }

        private void RegisterFlows()
        {

            RegisterFlow(CreatePlayerTowerBuildFlow);
            RegisterFlow(CreateSelectedTowerMaintenanceFlow);
            RegisterFlow(CreateTowerRoleFlow);
            RegisterFlow(CreateEnemyRoleFlow);

        }

        private void InitDirectors()
        {
            playerInputDirector.Init(this);
            towerBuildDirector.Init(this);
            enemyWaveDirector.Init(this);
        }

        #region FlowFactoy

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

        private TowerRoleFlow CreateTowerRoleFlow()
        {
            var flow = new TowerRoleFlow(enemyWaveDirector);
            return flow;
        }

        private EnemyRoleFlow CreateEnemyRoleFlow()
        {
            var flow = new EnemyRoleFlow(enemyWaveDirector,player.HPModule,player.GoldModule);
            return flow;
        }


        #endregion
    }
}