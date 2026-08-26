using Assets.Scripts.TowerDefenseGame.Weapon;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Feature
{

    public class TowerWeaponModule : MonoBehaviour
    {
        [SerializeField]
        private TowerStatModule towerStatModule;

        [SerializeField]
        private WeaponModule weaponModule;

        private int _weaponLevel;

        public void Init(TowerActor towerActor)
        {

            weaponModule.Init(towerActor);
        }

        public void ApplyLevel(int level)
        {
            _weaponLevel = level;
            UpdateStat();
        }

        public void UpdateStat()
        {
            var stat = new WeaponStat(_weaponLevel, towerStatModule.TotalDamage, towerStatModule.Rate, towerStatModule.Range);

            weaponModule.ApplyStat(stat);
        }

        public void StartFeature()
        {
            weaponModule.StartWeapon();
        }

    }
}
