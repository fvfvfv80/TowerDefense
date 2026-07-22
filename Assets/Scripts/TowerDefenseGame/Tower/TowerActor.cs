using System.Collections.Generic;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor, ITowerWeaponHandler
    {
        private TowerBaseModule _towerBase;

        private TowerWeaponModule _towerWeapon;

        private TowerDefenseTowerRole _towerRole;


        public TowerWeaponModule TowerWeaponModule => _towerWeapon;

        public void Init(TowerDefenseTowerRole towerRole)
        {
            _towerRole = towerRole;

           // _towerBase = GetComponent<TowerBaseModule>();
            _towerWeapon = GetComponent<TowerWeaponModule>();

            _towerWeapon.Init(this);
        }

        public void Setup()
        {
            _towerWeapon.Setup();
        }

        public IEnumerable<BaseActor> GetTargetList()
        {
            return _towerRole.FindTargetList();
        }
    }
}