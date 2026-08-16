using System;
using System.Collections.Generic;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor, ITowerModuleHost
    {
        private TowerBaseModule _towerBaseModule;

        private TowerWeaponModule _towerWeaponModule;

        private TowerUpgradeModule _towerUpgradeModule;

        private TowerRoleFlow _towerRoleFlow;



        public TowerBaseModule TowerBaseModule => _towerBaseModule;
        public TowerWeaponModule TowerWeaponModule => _towerWeaponModule;
        public TowerTemplateSO TowerTemplate => _towerBaseModule.TowerTemplate;


        public void Init()
        {
            _towerBaseModule = GetComponent<TowerBaseModule>();
            _towerWeaponModule = GetComponent<TowerWeaponModule>();
            _towerUpgradeModule = GetComponent<TowerUpgradeModule>();

            _towerWeaponModule.Init(this);
            _towerUpgradeModule.Init(this);
        }

        public void Setup()
        {
 
        }

        public void SetupRoleFlow(TowerRoleFlow towerRoleFlow)
        {
            _towerRoleFlow = towerRoleFlow;
        }

        public void StartTower()
        {
            _towerWeaponModule.StartTower();
        }
        
        public void UpgradeTower()
        {
            _towerUpgradeModule.UpgradeTower();
        }

        public void Release()
        {
            _towerRoleFlow.EndFlow();
        }

        #region TowerHandle
        public IEnumerable<BaseActor> GetTargetList()
        {
            return _towerRoleFlow.FindTargetList();
        }


        #endregion
    }
}