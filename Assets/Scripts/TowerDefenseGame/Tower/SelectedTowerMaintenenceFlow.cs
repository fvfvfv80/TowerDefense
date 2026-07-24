using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.UI;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class SelectedTowerMaintenanceFlow : MonoBehaviour
    {
        [SerializeField]
        private PlayerTowerBuildFlow towerBuildFlow;

        [SerializeField]
        private PlayerGoldModule playerGoldModule;
        [SerializeField]
        private PopupTowerUIViewer towerPopup;
        [SerializeField]
        private SystemTextViewer systemText;

        private TowerActor _currentTower;

        private bool _isSeleected;

        public bool IsSelected = true;

        public void SetSelectedTower(TowerActor towerActor)
        {
            _currentTower = towerActor;
            _isSeleected = true;
        }

        public void ShowTowerDetail()
        {
            towerPopup.OnPopup(_currentTower);
        }

        public void OnClickTowerUpgrade()
        {
            int cost = _currentTower.towerBaseModule.UpgradeCost;
            if (playerGoldModule.CheckGoldEnough(cost))
            {
                _currentTower.UpgradeTower();
                playerGoldModule.CurrentGold -= cost;

                towerPopup.UpdatePopup();
            }
            else
            {
                //실패 피드백
                systemText.PrintText(MESSAGE.MONEY);
            }
        }

        public void OnClickTowerSell()
        {
            towerBuildFlow.SellTower(_currentTower);
            towerPopup.OffPopup();

        }
    }
}