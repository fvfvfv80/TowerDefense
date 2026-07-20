using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame
{
    public class PlayerTowerBuildFlow : MonoBehaviour
    {
        [SerializeField]
        private TowerSpawner towerSpawner;

        [SerializeField]
        private PlayerGoldModule playerGold;

        [SerializeField]
        private EnemySystem enemySystem;

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
            var clone = towerSpawner.SpawnTower(tileTransform);

            clone.GetComponent<TowerWeaponModule>().Setup(enemySystem);

            playerGold.CurrentGold -= buildCost;

        }



    }
}