using Assets.Scripts.TowerDefenseGame.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{

    public class TowerWeaponModule : TowerFeatureModule, IWeaponModuleHost
    {
        [SerializeField] private TowerTemplateSO towerTemplate;
        [SerializeField] private WeaponModule weaponModule;

        private TowerActor _towerActor;

        public override void Init(TowerActor towerActor)
        {
            _towerActor = towerActor;

            weaponModule.Init(this);
        }

        public override void ApplyLevel(int level)
        {
            var levelData = towerTemplate.weapon[level];

            var stat = new WeaponStat(level, levelData.damage, levelData.rate, levelData.range);

            weaponModule.ApplyStat(stat);
        }

        public override void StartFeature()
        {
            weaponModule.StartWeapon();
        }

        public IEnumerable<BaseActor> RequestTargetList()
        {
            return _towerActor.RequestTowerTargetList();
        }
    }
}
