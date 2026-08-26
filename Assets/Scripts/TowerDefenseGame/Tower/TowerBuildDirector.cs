using Assets.Scripts.TowerDefenseGame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{

    public interface ITowerBuildScenario
    {
        void NotifyTowerBuilt(TowerActor towerActor);
        void NotifyTowerDemolished(TowerActor towerActor);
    }

    public class TowerBuildDirector : MonoBehaviour
    {

        [SerializeField]
        private TowerTemplateSO[] towerTemplates;

        [SerializeField]
        private TowerSpawnModule towerSpawnModule;

        [SerializeField]
        private PopupTowerUIViewer towerPopup;

        [SerializeField]
        private SystemTextViewer systemTextViewer;

        private ITowerBuildScenario _towerBuildScenario;

        private readonly Dictionary<TowerActor, Tile> _towerPlacementDict = new();

        public void Init(ITowerBuildScenario scenario)
        {
            _towerBuildScenario = scenario;
        }

        public bool CheckTowerBuildCostEnough(int towerType, int gold)
        {
            var towerCost = towerTemplates[towerType].weapon[0].cost;
            if (towerCost > gold)
            {
                systemTextViewer.PrintText(MESSAGE.MONEY);
                return false;
            }

            return true;
        }

        public bool TryBuildTower(int towerType, Transform tileTransform, out int buildCost)
        {
            var tile = tileTransform.GetComponent<Tile>();
            buildCost = 0;
            if (tile.IsBuildTower)
            {

                systemTextViewer.PrintText(MESSAGE.BUILD);

                return false;
            }

            tile.IsBuildTower = true;

            var towerPrefab = towerTemplates[towerType].towerPrefab;

            var towerActor = towerSpawnModule.SpawnTower(towerPrefab, tile.transform);

            _towerBuildScenario.NotifyTowerBuilt(towerActor);
            towerActor.StartTower();

            _towerPlacementDict[towerActor] = tile;

            buildCost = towerTemplates[towerType].weapon[0].cost;

            return true;
        }

        public bool TryDemolishTower(TowerActor towerActor, out int sellPrice)
        {

            sellPrice = towerActor.SellCost;

            _towerPlacementDict[towerActor].IsBuildTower = false;
            _towerPlacementDict.Remove(towerActor);

            _towerBuildScenario.NotifyTowerDemolished(towerActor);

            towerSpawnModule.DespawnTower(towerActor);

            return true;
        }

        public GameObject SpawnFollowTowerPreview(int towerType)
        {
            var clone = Instantiate(towerTemplates[towerType].followTowerPrefab);
            return clone;
        }

        public void ShowTowerDetail(TowerActor towerActor)
        {
            towerPopup.ShowPopup(towerActor);
        }

        public bool TryUpgradeTower(TowerActor tower, int currentGold, out int upgradeCost)
        {

            upgradeCost = tower.UpgradeCost;

            if (tower.IsMaxLevel)
            {
                return false;
            }

            if (currentGold < upgradeCost)
            {

                systemTextViewer.PrintText(MESSAGE.MONEY);

                return false;

            }

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