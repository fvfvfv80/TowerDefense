using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.SceneRoot;
using Assets.Scripts.TowerDefenseGame.Tower;
using System;
using UnityEngine;



namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerInputDirector : MonoBehaviour, IPlayerInputHost
    {

        [SerializeField]
        private PlayerInputGameplay playerInputGameplay;

        private PlayerTowerBuildFlow _playerTowerBuildFlow;

        private SelectedTowerMaintenanceFlow _selectedTowerMaintenanceFlow;

        private IFlowCreator _flowCreator;


        private bool IsBuilding => _playerTowerBuildFlow != null;
        private bool IsMaintaining => _selectedTowerMaintenanceFlow != null;


        private void Awake()
        {
     
        }


        public void Init(IFlowCreator flowCreator)
        {
            _flowCreator = flowCreator;

            playerInputGameplay = GetComponent<PlayerInputGameplay>();

            playerInputGameplay.Init(this);
        }




        //인풋 모듈이면 여기 있는게 자연스럽고 게임플레이면 아래 메소드들은 게임플레이쪽이 자연스러울지도..
        #region HandlePlayerInput 

        public void RequestEnterTowerBuild()
        {
            HandleBuildEnterRequest();
        }

        public void RequestBuildTower(Transform tileTransform)
        {
            _playerTowerBuildFlow?.BuildTower(tileTransform);
        }

        public void RequestSelectTower(Transform towerTransform)
        {
            if (IsBuilding)
                return;

            var towerActor = towerTransform.GetComponent<TowerActor>();

            _selectedTowerMaintenanceFlow = _flowCreator.CreateFlow<SelectedTowerMaintenanceFlow>();

            _selectedTowerMaintenanceFlow.Completed += HandleMaintenanceFlowEnd;

            _selectedTowerMaintenanceFlow.StartMaintenance(towerActor);

        }



        public void RequestUpgradeTower()
        {
            _selectedTowerMaintenanceFlow.UpgradeTower();
        }

        public void RequestSellTower()
        {
            _selectedTowerMaintenanceFlow.SellTower();

        }


        public void RequestCancelPlayerAction()
        {
            //정비중이면 먼저 꺼지게?
            if (IsMaintaining)
            {
                EndMaintenanceFlow();
            }
            else if (IsBuilding)
            {
                EndBuildFlow();
            }
          
        }



        #endregion


        private void HandleBuildEnterRequest()
        {
            //진행중인 플로우 종료
            EndMaintenanceFlow();
            EndBuildFlow();

            _playerTowerBuildFlow = _flowCreator.CreateFlow<PlayerTowerBuildFlow>();

            _playerTowerBuildFlow.Completed += HandleBuildFlowComplete;

            _playerTowerBuildFlow.EnterBuildReady();

        }

        private void HandleBuildFlowComplete()
        {
            EndBuildFlow();
        }

        private void HandleMaintenanceFlowEnd()
        {
            EndMaintenanceFlow();
        }


        private void EndBuildFlow()
        {
            _playerTowerBuildFlow?.EndFlow();
            _playerTowerBuildFlow = null;
        }

        private void EndMaintenanceFlow()
        {
            _selectedTowerMaintenanceFlow?.EndFlow();
            _selectedTowerMaintenanceFlow = null;
        }



    }

}