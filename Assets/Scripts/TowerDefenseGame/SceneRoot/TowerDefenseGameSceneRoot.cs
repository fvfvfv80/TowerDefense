using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Scenario;
using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.SceneRoot
{
    public class TowerDefenseGameSceneRoot : BaseSceneRoot
    {
        [SerializeField]
        private PlayerActionDirector playerActionDirector;

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

            RegisterScenarioCreation(CreateTowerScenario);
            RegisterScenarioCreation(CreateEnemyScenario);

        }

        private void InitDirectors()
        {

            var playerActionScenario = new PlayerActionScenario(this);
            playerActionDirector.Init(playerActionScenario);

            var towerBuildScenario = new TowerBuildScenario(this);
            towerBuildDirector.Init(towerBuildScenario);

            var enemyWaveScenario = new EnemyWaveScenario(this);
            enemyWaveDirector.Init(enemyWaveScenario);

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

        #endregion
        #region ScenarioFactory

        private TowerScenario CreateTowerScenario()
        {
            var scenario = new TowerScenario(enemyWaveDirector, towerBuffDirector);
            return scenario;
        }

        private EnemyScenario CreateEnemyScenario()
        {
            var scenario = new EnemyScenario(enemyWaveDirector, player.HPModule, player.GoldModule);
            return scenario;
        }
        #endregion

    }
}