using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Player;
using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.SceneRoot
{
    public class PlayerTowerBuildFlow : IFlow
    {

        private readonly TowerBuildDirector _towerBuildDirector;

        private readonly PlayerGoldModule _playerGoldModule;

        private GameObject _followTowerPreview;

        public bool IsRunning { get; private set; } = true;


        public PlayerTowerBuildFlow(TowerBuildDirector towerBuildDirector,PlayerGoldModule playerGold)
        {
            _towerBuildDirector = towerBuildDirector;
            _playerGoldModule = playerGold;
        }

        public void EnterBuildReady()
        {
            if (!_towerBuildDirector.CheckGoldEnough(_playerGoldModule.CurrentGold))
            {
                EndFlow();
                return;
            }
                
            
            _followTowerPreview = _towerBuildDirector.SpawnFollowTowerPreview();

        }

        public void BuildTower(Transform tileTransform)
        {
            if (!IsRunning)
                return;


            if(_towerBuildDirector.TryBuildTower(tileTransform, out int cost))
            {
                _playerGoldModule.CurrentGold -= cost;
                
                EndFlow();
            }
        }



        public void EndFlow()
        {
            //프리팹 Release할거있으면 하기
            if(_followTowerPreview)
                Object.Destroy(_followTowerPreview);
            IsRunning = false;
        }


    }
}