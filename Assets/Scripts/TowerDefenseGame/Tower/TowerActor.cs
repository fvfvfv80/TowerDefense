using System;
using System.Collections.Generic;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor, ITowerWeaponHost
    {
        private TowerBaseModule _towerBaseModule;

        private TowerWeaponModule _towerWeaponModule;

        private TowerUpgradeGameplay _towerUpgradeGameplay;

        private TowerContext _towerContext;



        public TowerBaseModule TowerBaseModule => _towerBaseModule;
        public TowerWeaponModule TowerWeaponModule => _towerWeaponModule;
        public TowerTemplateSO TowerTemplate => _towerBaseModule.TowerTemplate;


        public void Init()
        {
            _towerBaseModule = GetComponent<TowerBaseModule>();
            _towerWeaponModule = GetComponent<TowerWeaponModule>();
            _towerUpgradeGameplay = GetComponent<TowerUpgradeGameplay>();

            _towerWeaponModule.Init(this);

        }

        public void Setup()
        {
 
        }

        public void SetupContext(TowerContext towerContext)
        {
            _towerContext = towerContext;
        }

        public void StartTower()
        {
            _towerWeaponModule.StartTower();
        }
        
        public void UpgradeTower()
        {
            _towerUpgradeGameplay.UpgradeTower();
        }

        public void ReleaseContext()
        {
            _towerContext.Release();
        }

        #region TowerHandle
        public IEnumerable<BaseActor> GetTargetList()
        {
            return _towerContext.FindTargetList();
        }


        #endregion
    }
}