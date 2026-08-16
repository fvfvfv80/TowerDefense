using Assets.Scripts.TowerDefenseGame.Tower;
using Assets.Scripts.TowerDefenseGame.UI;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Legacy
{

    //타워 빌드 디렉터랑 함께 매니지먼트 디렉터로도 합치기 가능
    public class TowerMaintenanceDirector : MonoBehaviour
    {
        [SerializeField]
        private PopupTowerUIViewer towerPopup;


        [SerializeField]
        private SystemTextViewer systemTextViewer;


        public void ShowTowerDetail(TowerActor towerActor)
        {
            towerPopup.ShowPopup(towerActor);
        }


        public bool TryUpgradeTower(TowerActor tower, int currentGold, out int upgradeCost)
        {

            int cost = tower.TowerBaseModule.UpgradeCost;
            upgradeCost = 0;
            if (currentGold < cost)
            {
                //실패 피드백
                systemTextViewer.PrintText(MESSAGE.MONEY);
           
                return false;

            }

            upgradeCost = tower.TowerBaseModule.CurrentTowerWeaponData.cost;

            tower.UpgradeTower();
            towerPopup.UpdatePopup();

        

            return true;
        }


        public void HideTowerDetail()
        {
            towerPopup.OffPopup();
        }
    }
}