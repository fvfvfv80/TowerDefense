using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Scenario;
using UnityEngine;



namespace Assets.Scripts.TowerDefenseGame.Player
{


     
    public class PlayerInputDirector : MonoBehaviour, IPlayerInputHost
    {

        [SerializeField]
        private PlayerInputGameplay playerInputGameplay;

        private PlayerInputScenario _playerInputScenario;


        private void Awake()
        {
     
        }


        public void Init(PlayerInputScenario scenario)
        {
            _playerInputScenario = scenario;

            playerInputGameplay = GetComponent<PlayerInputGameplay>();

            playerInputGameplay.Init(this);
        }



        //인풋 모듈이면 여기 있는게 자연스럽고 게임플레이면 아래 메소드들은 게임플레이쪽이 자연스러울지도..
        #region HandlePlayerInput 

        public void RequestEnterTowerBuild(int towerType)
        {
            _playerInputScenario.StartBuild(towerType);
        }

        public void RequestBuildTower(Transform tileTransform)
        {
            _playerInputScenario.BuildTower(tileTransform);
        }

        public void RequestSelectTower(Transform towerTransform)
        {
            _playerInputScenario.SelectTower(towerTransform);

        }

        public void RequestUpgradeTower()
        {
            _playerInputScenario.UpgradeTower();
        }

        public void RequestSellTower()
        {
            _playerInputScenario.SellTower();

        }

        public void RequestCancelPlayerAction()
        {
            _playerInputScenario.CancelPlayerAction();

        }
        #endregion


    


    }

}