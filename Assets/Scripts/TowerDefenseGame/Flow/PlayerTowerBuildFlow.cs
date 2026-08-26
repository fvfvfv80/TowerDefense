using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Flow
{
    public class PlayerTowerBuildFlow : IFlow
    {

        private readonly TowerBuildDirector _towerBuildDirector;

        private readonly PlayerGoldModule _playerGoldModule;

        private GameObject _followTowerPreview;


        private int _selectedTowerType;


        public event Action Completed;


        public PlayerTowerBuildFlow(TowerBuildDirector towerBuildDirector,PlayerGoldModule playerGold)
        {
            _towerBuildDirector = towerBuildDirector;
            _playerGoldModule = playerGold;
        }

        public void EnterBuildReady(int towerType)
        {
            if (!_towerBuildDirector.CheckTowerBuildCostEnough(towerType,_playerGoldModule.CurrentGold))
            {
                Completed?.Invoke();
                return;
            }

            _selectedTowerType = towerType;

            _followTowerPreview = _towerBuildDirector.SpawnFollowTowerPreview(towerType);

        }

        public void BuildTower(Transform tileTransform)
        {
            if(_towerBuildDirector.TryBuildTower(_selectedTowerType,tileTransform, out int cost))
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