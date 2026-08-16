using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.SceneRoot;
using Assets.Scripts.TowerDefenseGame.Tower;
using UnityEngine;



namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerInputDirector : MonoBehaviour, IPlayerInputHost
    {

        [SerializeField]
        private PlayerInputModule playerInputModule;

        private PlayerTowerBuildFlow _playerTowerBuildFlow;

        private SelectedTowerMaintenanceFlow _selectedTowerMaintenanceFlow;

        private IFlowCreator _flowCreator;


        private void Awake()
        {
     
        }


        public void Init(IFlowCreator flowCreator)
        {
            _flowCreator = flowCreator;

            playerInputModule = GetComponent<PlayerInputModule>();

            playerInputModule.Init(this);
        }




        //인풋 모듈이면 여기 있는게 자연스럽고 게임플레이면 아래 메소드들은 게임플레이쪽이 자연스러울지도..
        #region HandlePlayerInput 
        public void RequesteBuildTower(Transform tileTransform)
        {
            

            _playerTowerBuildFlow.BuildTower(tileTransform);
        }

        public void RequestSelectTower(Transform towerTransform)
        {
            if (_playerTowerBuildFlow.IsRunning)
                return;

            var towerActor = towerTransform.GetComponent<TowerActor>();

            _selectedTowerMaintenanceFlow = _flowCreator.CreateFlow<SelectedTowerMaintenanceFlow>();

            _selectedTowerMaintenanceFlow.StartMaintenance(towerActor);

        }
        public void RequestEnterTowerBuild()
        {
            HandleBuildTowerButton();
        }

        private void HandleBuildTowerButton()
        {
            //메인테넌스플로우가 진행중이면
            _selectedTowerMaintenanceFlow?.EndMaintenance();
            _playerTowerBuildFlow?.EndFlow();

            _playerTowerBuildFlow = _flowCreator.CreateFlow<PlayerTowerBuildFlow>();

            _playerTowerBuildFlow.EnterBuildReady();

        }

        public void RequestCancelTowerBuild()
        {
            _playerTowerBuildFlow?.EndFlow();

        }

        public void RequestUpgradeTower()
        {
            _selectedTowerMaintenanceFlow.UpgradeTower();
        }

        public void RequestSellTower()
        {
            _selectedTowerMaintenanceFlow.SellTower();

        }

        public void RequestCancelTowerMaintenance()
        {
            _selectedTowerMaintenanceFlow.EndMaintenance();

        }



        #endregion




    }

}