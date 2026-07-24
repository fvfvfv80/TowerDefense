using System.Collections.Generic;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor, ITowerWeaponHandler
    {
        private TowerBaseModule _towerBase;

        private TowerWeaponModule _towerWeapon;

        private TowerDefenseTowerRole _towerRole;


        public TowerBaseModule towerBaseModule => _towerBase;
        public TowerWeaponModule TowerWeaponModule => _towerWeapon;

        public TowerTemplateSO TowerTemplate => _towerBase.TowerTemplate;

        public void Init(TowerDefenseTowerRole towerRole)
        {
            _towerRole = towerRole;

            _towerBase = GetComponent<TowerBaseModule>();
            _towerWeapon = GetComponent<TowerWeaponModule>();

            _towerWeapon.Init(this);
        }

        public void Setup()
        {
            _towerWeapon.Setup();
        }

        public void UpgradeTower()
        {
            _towerBase.Upgrade();
            _towerWeapon.Upgrade();
        }

        public IEnumerable<BaseActor> GetTargetList()
        {
            return _towerRole.FindTargetList();
        }
    }
}