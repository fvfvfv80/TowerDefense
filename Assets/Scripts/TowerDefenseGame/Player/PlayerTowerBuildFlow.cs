using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.Tower;
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
        private int buildCost = 50;


        public void TryBuildTower(Transform tileTransform)
        {
            if (buildCost > playerGold.CurrentGold)
                return;

            var tile = tileTransform.GetComponent<Tile>();

            if (tile.IsBuildTower)
                return;

            tile.IsBuildTower = true;

            //선택한 위치에 타워 생성
            var towerActor = towerSpawner.SpawnTower(tileTransform);

            playerGold.CurrentGold -= buildCost;

        }



    }
}