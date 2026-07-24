using Assets.Scripts.TowerDefenseGame.Tower;
using Assets.Scripts.TowerDefenseGame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerTowerBuildFlow : MonoBehaviour
    {
        [SerializeField]
        private TowerSpawnFlow towerSpawner;

        [SerializeField]
        private PlayerGoldModule playerGold;

        [SerializeField]
        private SystemTextViewer systemText;

        private Dictionary<TowerActor, Tile> _towerPlacementDict = new();


        public void TryBuildTower(Transform tileTransform)
        {
            //아마 어떤 타워를 지을지 이미 알고 있을가능성이높음
            if (!playerGold.CheckGoldEnough(towerSpawner.BuildCost))
            {
                systemText.PrintText(MESSAGE.MONEY);
                return;
            }
                

            var tile = tileTransform.GetComponent<Tile>();

            if (tile.IsBuildTower)
            {
                systemText.PrintText(MESSAGE.BUILD);
                return;
            }
               

            tile.IsBuildTower = true;

            //선택한 위치에 타워 생성
            var towerActor = towerSpawner.SpawnTower(tile.transform);

            _towerPlacementDict[towerActor] = tile;

            playerGold.CurrentGold -= towerSpawner.BuildCost;

        }


        public void SellTower(TowerActor towerActor)
        {
            _towerPlacementDict[towerActor].IsBuildTower = false;

            playerGold.CurrentGold += towerActor.towerBaseModule.SellGold;

            towerSpawner.DespawnTower(towerActor);
        }



    }
}