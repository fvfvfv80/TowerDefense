using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class TowerDefensePlayerRole : MonoBehaviour
    {
        [SerializeField]
        private SelectedTowerMaintenanceFlow towerMaintenanceFlow;

        [SerializeField]
        private PlayerTowerBuildFlow playerTowerBuildFlow;

        
        public void SelectTower(Transform towerTransform)
        {
            var towerActor = towerTransform.GetComponent<TowerActor>();

            towerMaintenanceFlow.SetSelectedTower(towerActor);
            towerMaintenanceFlow.ShowTowerDetail();
        }



        public void TryBuildTower(Transform tileTransform)
        {
            playerTowerBuildFlow.TryBuildTower(tileTransform);
        }
    }
}