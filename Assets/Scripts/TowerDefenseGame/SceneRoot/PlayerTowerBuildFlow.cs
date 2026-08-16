using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.SceneRoot
{
    public class PlayerTowerBuildFlow : IFlow
    {

        private readonly TowerBuildDirector _towerBuildDirector;

        private readonly PlayerGoldModule _playerGoldModule;

        private GameObject _followTowerPreview;



        public event Action Completed;


        public PlayerTowerBuildFlow(TowerBuildDirector towerBuildDirector,PlayerGoldModule playerGold)
        {
            _towerBuildDirector = towerBuildDirector;
            _playerGoldModule = playerGold;
        }

        public void EnterBuildReady()
        {
            if (!_towerBuildDirector.CheckGoldEnough(_playerGoldModule.CurrentGold))
            {
                Completed?.Invoke();
                return;
            }
                
            
            _followTowerPreview = _towerBuildDirector.SpawnFollowTowerPreview();

        }

        public void BuildTower(Transform tileTransform)
        {
            if(_towerBuildDirector.TryBuildTower(tileTransform, out int cost))
            {
                _playerGoldModule.CurrentGold -= cost;

                Completed?.Invoke();
            }
        }



        public void EndFlow()
        {
            //프리팹 Release할거있으면 하기
            if (_followTowerPreview != null)
                GameObject.Destroy(_followTowerPreview);
        }


    }
}