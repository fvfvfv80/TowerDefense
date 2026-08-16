using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{


    public class TowerBuildDirector : MonoBehaviour
    {

        [SerializeField]
        private SystemTextViewer systemTextViewer;

        [SerializeField]
        private TowerTemplateSO towerData;

        [SerializeField]
        private TowerSpawnModule towerSpawnModule;

        private IFlowCreator _flowCreator;


        private readonly Dictionary<TowerActor, Tile> _towerPlacementDict = new();


        public int TowerCost => towerData.weapon[0].cost;


        public void Init(IFlowCreator flowCreator)
        {
            _flowCreator = flowCreator;
        }

        public bool CheckGoldEnough(int gold)
        {
            if(TowerCost>gold)
            {
                systemTextViewer.PrintText(MESSAGE.MONEY);
                return false;
            }

            return true;
        }


        public bool TryBuildTower(Transform tileTransform, out int buildCost)
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


            //선택한 위치에 타워 생성
            var towerActor = towerSpawnModule.SpawnTower(tile.transform);

            //타워 스폰 플로우로 생성받기
            var towerRoleFlow = _flowCreator.CreateFlow<TowerRoleFlow>();
            towerActor.SetupRoleFlow(towerRoleFlow);
            towerActor.StartTower();

            _towerPlacementDict[towerActor] = tile;


            //결과 반환
            buildCost = TowerCost;

            return true;
        }

       
        public bool TryDemolishTower(TowerActor towerActor, out int sellPrice)
        {
            _towerPlacementDict[towerActor].IsBuildTower = false;

            towerSpawnModule.DespawnTower(towerActor);

            sellPrice = towerData.weapon[towerActor.TowerBaseModule.Level].sell;


            return true;
        }


        public GameObject SpawnFollowTowerPreview()
        {
            var clone = Instantiate(towerData.followTowerPrefab);
            return clone;
        }





        //메인테넌스 디렉터 합쳐보기//////////////////////////////////////////////////////////////

        [SerializeField]
        private PopupTowerUIViewer towerPopup;


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