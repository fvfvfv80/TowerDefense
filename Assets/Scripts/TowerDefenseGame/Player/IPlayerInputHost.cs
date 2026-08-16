using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{

    
    public interface IPlayerInputHost
    {
        void RequestBuildTower(Transform tileTransform);
        void RequestSelectTower(Transform towerTransform);

        void RequestEnterTowerBuild();

        void RequestUpgradeTower();

        void RequestSellTower();

        void RequestCancelPlayerAction();
    }
}
