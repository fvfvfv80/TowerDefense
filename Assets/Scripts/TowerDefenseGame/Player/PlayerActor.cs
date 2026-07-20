
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerActor : BaseActor, IPlayerHandler
    {
        [SerializeField]
        private PlayerTowerBuildFlow towerBuildFlow;

        private PlayerHPModule _playerHP;

        private PlayerBuildInputModule _playerBuildInput;

        private PlayerGoldModule _playerGold;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _playerHP = GetComponent<PlayerHPModule>();
            _playerBuildInput = GetComponent<PlayerBuildInputModule>();
            _playerGold = GetComponent<PlayerGoldModule>();

            _playerBuildInput.Init(this);
        }
        #region PlayerHandle
        public void BuildTower(Transform tileTransform)
        {
            towerBuildFlow.TryBuildTower(tileTransform);
        }
        #endregion
    }
}