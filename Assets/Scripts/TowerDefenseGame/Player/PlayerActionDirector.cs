using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{

    public interface IPlayerActionScenario
    {
        void BeginTowerBuild(int towerType);
        void BuildTower(Transform tileTransform);
        void SelectTower(Transform towerTransform);
        void UpgradeTower();
        void SellTower();
        void CancelPlayerAction();
    }

    public class PlayerActionDirector : MonoBehaviour, IPlayerInputHost
    {

        [SerializeField]
        private PlayerInputGameplay playerInputGameplay;

        private IPlayerActionScenario _playerActionScenario;

        public void Init(IPlayerActionScenario scenario)
        {
            _playerActionScenario = scenario;

            playerInputGameplay = GetComponent<PlayerInputGameplay>();

            playerInputGameplay.Init(this);
        }

        #region IPlayerInputHost

        public void RequestEnterTowerBuild(int towerType)
        {
            _playerActionScenario.BeginTowerBuild(towerType);
        }

        public void RequestBuildTower(Transform tileTransform)
        {
            _playerActionScenario.BuildTower(tileTransform);
        }

        public void RequestSelectTower(Transform towerTransform)
        {
            _playerActionScenario.SelectTower(towerTransform);

        }

        public void RequestUpgradeTower()
        {
            _playerActionScenario.UpgradeTower();
        }

        public void RequestSellTower()
        {
            _playerActionScenario.SellTower();

        }

        public void RequestCancelPlayerAction()
        {
            _playerActionScenario.CancelPlayerAction();

        }
        #endregion

    }

}