using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{

    public class TowerBuildDirectorContext: IContext
    {
        private readonly IFlowCreator _flowCreator;

        private readonly IContextCreator _contextCreator;

        public TowerBuildDirectorContext(IFlowCreator flowCreator, IContextCreator contextCreator)
        {
            _flowCreator = flowCreator;
            _contextCreator = contextCreator;
        }

        public TowerContext CreateTowerContext()
        {
            return _contextCreator.CreateContext<TowerContext>();
        }
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

        private TowerBuildDirectorContext _context;


        private readonly Dictionary<TowerActor, Tile> _towerPlacementDict = new();



        public void Init(TowerBuildDirectorContext context)
        {
            _context = context;
        }

        public bool CheckTowerBuildCostEnough(int towerType,int gold)
        {
            var towerCost = towerTemplates[towerType].weapon[0].cost;
            if(towerCost > gold)
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
                //빌드 사유가 다양해지면 result 반환하기 //flow에서 메시지를 반영하게 만들기도가능
                systemTextViewer.PrintText(MESSAGE.BUILD);
                
                return false;
            }

            tile.IsBuildTower = true;

            var towerPrefab = towerTemplates[towerType].towerPrefab;

            //선택한 위치에 타워 생성
            var towerActor = towerSpawnModule.SpawnTower(towerPrefab,tile.transform);

            //타워 스폰 플로우로 생성받기
            var towerContext = _context.CreateTowerContext();
            towerActor.SetupContext(towerContext);
            towerActor.StartTower();

            _towerPlacementDict[towerActor] = tile;


            //결과 반환
            buildCost = towerTemplates[towerType].weapon[0].cost;

            return true;
        }

       
        public bool TryDemolishTower(TowerActor towerActor, out int sellPrice)
        {

            sellPrice = towerActor.SellGold;


            _towerPlacementDict[towerActor].IsBuildTower = false;
            _towerPlacementDict.Remove(towerActor);

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
            if (currentGold < upgradeCost)
            {
                //실패 피드백
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