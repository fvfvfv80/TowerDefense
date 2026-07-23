using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerActor : BaseActor, IPlayerHandler
    {
        [SerializeField]
        private TowerDefensePlayerRole playerRole;

        private PlayerHPModule _playerHP;

        private PlayerInputModule _playerBuildInput;

        private PlayerGoldModule _playerGold;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _playerHP = GetComponent<PlayerHPModule>();
            _playerBuildInput = GetComponent<PlayerInputModule>();
            _playerGold = GetComponent<PlayerGoldModule>();

            _playerBuildInput.Init(this);
        }
        #region PlayerHandle
        public void BuildTower(Transform tileTransform)
        {
            playerRole.TryBuildTower(tileTransform);
        }

        public void SelectTower(Transform towerTransform)
        {
            playerRole.ShowTowerDetail(towerTransform);
        }
        #endregion
    }
}