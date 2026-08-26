using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using System;

namespace Assets.Scripts.TowerDefenseGame.Flow
{
    public class SelectedTowerMaintenanceFlow : IFlow
    {
        private PlayerGoldModule _playerGoldModule;

        private TowerBuildDirector _towerBuildDirector;

        private TowerActor _selectedTower;

        public event Action Completed;

        public SelectedTowerMaintenanceFlow(TowerBuildDirector towerBuildDirector, PlayerGoldModule playerGoldModule)
        {
            _towerBuildDirector = towerBuildDirector;

            _playerGoldModule = playerGoldModule;
        }

        public void StartMaintenance(TowerActor towerActor)
        {
            _selectedTower = towerActor;

            _towerBuildDirector.ShowTowerDetail(_selectedTower);
        }

        public void SellTower()
        {
            var result = _towerBuildDirector.TryDemolishTower(_selectedTower, out var sellPrice);
            if (result)
            {
                //플레이어 골드 증가
                _playerGoldModule.CurrentGold += sellPrice;

            }

            Completed?.Invoke();

        }

        public void UpgradeTower()
        {
            var result = _towerBuildDirector.TryUpgradeTower(_selectedTower, _playerGoldModule.CurrentGold, out var upgradeCost);
            if (result)
            {
                //플레이어 골드 감소
                _playerGoldModule.CurrentGold -= upgradeCost;
            }
        }

        public void EndFlow()
        {
            _selectedTower = null;
            _towerBuildDirector.HideTowerDetail();
        }

    }
}