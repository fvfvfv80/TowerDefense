using Assets.Scripts.TowerDefenseGame.Weapon;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Feature
{


    public class TowerWeaponModule : TowerFeatureModule
    {
        [SerializeField]
        private TowerStatModule towerStatModule;

        [SerializeField] 
        private WeaponModule weaponModule;


        public override void Init(TowerActor towerActor)
        {

            weaponModule.Init(towerActor);
        }

        public override void ApplyLevel(int level)
        {
            var stat = new WeaponStat(level, towerStatModule.TotalDamage, towerStatModule.Rate, towerStatModule.Range);

            weaponModule.ApplyStat(stat);
        }

        public override void StartFeature()
        {
            weaponModule.StartWeapon();
        }


    }
}
