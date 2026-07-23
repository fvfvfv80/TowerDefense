using Assets.Scripts.TowerDefenseGame.UI;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class TowerDefensePlayerRole : MonoBehaviour
    {
        [SerializeField]
        private PlayerTowerBuildFlow playerTowerBuildFlow;

        [SerializeField]
        private PopupTowerUIViewer popupTower;

        public void ShowTowerDetail(Transform towerTransform)
        {
            popupTower.OnPopup(towerTransform);
        }

        public void TryBuildTower(Transform tileTransform)
        {
            playerTowerBuildFlow.TryBuildTower(tileTransform);
        }
    }
}