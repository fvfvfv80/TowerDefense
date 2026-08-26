using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class PlayerActionScenario : IPlayerActionScenario
    {
        private readonly IScenarioContext _scenarioContext;

        private PlayerTowerBuildFlow _playerTowerBuildFlow;

        private SelectedTowerMaintenanceFlow _selectedTowerMaintenanceFlow;


        private bool IsBuilding => _playerTowerBuildFlow != null;
        private bool IsMaintaining => _selectedTowerMaintenanceFlow != null;



        public PlayerActionScenario(IScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        public void BeginTowerBuild(int towerType)
        {
            EndMaintenanceFlow();
            EndBuildFlow();

            _playerTowerBuildFlow = _scenarioContext.CreateFlow<PlayerTowerBuildFlow>();

            _playerTowerBuildFlow.Completed += HandleBuildFlowComplete;

            _playerTowerBuildFlow.EnterBuildReady(towerType);

        }

        public void BuildTower(Transform tileTransform)
        {
            _playerTowerBuildFlow?.BuildTower(tileTransform);
        }

        public void SelectTower(Transform towerTransform)
        {
            if (IsBuilding)
                return;

            var towerActor = towerTransform.GetComponent<TowerActor>();

            _selectedTowerMaintenanceFlow = _scenarioContext.CreateFlow<SelectedTowerMaintenanceFlow>();

            _selectedTowerMaintenanceFlow.Completed += HandleMaintenanceFlowEnd;

            _selectedTowerMaintenanceFlow.StartMaintenance(towerActor);
        }

        public void UpgradeTower()
        {
            _selectedTowerMaintenanceFlow.UpgradeTower();
        }

        public void SellTower()
        {
            _selectedTowerMaintenanceFlow.SellTower();

        }

        public void CancelPlayerAction()
        {
            if (IsMaintaining)
            {
                EndMaintenanceFlow();
            }
            else if (IsBuilding)
            {
                EndBuildFlow();
            }

        }

        private void HandleBuildFlowComplete()
        {
            EndBuildFlow();
        }

        private void HandleMaintenanceFlowEnd()
        {
            EndMaintenanceFlow();
        }

        private void EndBuildFlow()
        {
            _playerTowerBuildFlow?.EndFlow();
            _playerTowerBuildFlow = null;
        }

        private void EndMaintenanceFlow()
        {
            _selectedTowerMaintenanceFlow?.EndFlow();
            _selectedTowerMaintenanceFlow = null;
        }


    }
}
