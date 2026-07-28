using Assets.Scripts.TowerDefenseGame.Tower;
using Assets.Scripts.TowerDefenseGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerTowerBuildFlow : MonoBehaviour
    {


        [SerializeField]
        private TowerSpawnFlow towerSpawner;

        [SerializeField]
        private PlayerGoldModule playerGold;

        [SerializeField]
        private PlayerInputModule playerInput;

        [SerializeField]
        private SystemTextViewer systemText;

        private Dictionary<TowerActor, Tile> _towerPlacementDict = new();


        private bool _isOnTowerButton = false;
        private GameObject _followTowerClone = null;

        public void ReadyToBuildTower()
        {
            //아마 어떤 타워를 지을지 이미 알고 있을가능성이높음
            if (!playerGold.CheckGoldEnough(towerSpawner.BuildCost))
            {
                systemText.PrintText(MESSAGE.MONEY);
                return;
            }

            _followTowerClone = towerSpawner.SpawnFollowTower();
            _followTowerClone.GetComponent<ObjectFollowMousePositionModule>().Setup(playerInput);

            _isOnTowerButton = true;

            StartCoroutine(nameof(OnTowerCancelProcess));
        }

        public void TryBuildTower(Transform tileTransform)
        {

            if (!_isOnTowerButton)
                return;

            var tile = tileTransform.GetComponent<Tile>();

            if (tile.IsBuildTower)
            {
                systemText.PrintText(MESSAGE.BUILD);
                return;
            }

            _isOnTowerButton = false;

            tile.IsBuildTower = true;

            //선택한 위치에 타워 생성
            var towerActor = towerSpawner.SpawnTower(tile.transform);

            _towerPlacementDict[towerActor] = tile;

            playerGold.CurrentGold -= towerSpawner.BuildCost;

            Destroy(_followTowerClone);

            StopCoroutine(nameof(OnTowerCancelProcess));

        }


        public void SellTower(TowerActor towerActor)
        {
            _towerPlacementDict[towerActor].IsBuildTower = false;

            playerGold.CurrentGold += towerActor.towerBaseModule.SellGold;

            towerSpawner.DespawnTower(towerActor);
        }

        private IEnumerator OnTowerCancelProcess()
        {
            while (true)
            {
                if (Keyboard.current.escapeKey.wasPressedThisFrame ||
                    Mouse.current.rightButton.wasPressedThisFrame)
                {
                    _isOnTowerButton = false;
                    Destroy(_followTowerClone);
                    break;
                }
                yield return null;
            }
           
        }


    }
}