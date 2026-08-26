using Assets.Scripts.TowerDefenseGame.Tower.Feature;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Gameplay
{
    public class AttackTowerGameplay : TowerGameplay
    {

        [SerializeField]
        private TowerWeaponModule towerWeaponModule;

        public override void Init(TowerActor towerActor)
        {

            towerWeaponModule.Init(towerActor);
        }
        public override void Setup()
        {
            towerWeaponModule.ApplyLevel(0);
        }
        public override void ApplyLevel(int level)
        {
            towerWeaponModule.ApplyLevel(level);
        }

        public override void UpdateStat()
        {
            towerWeaponModule.UpdateStat();
        }

        public override void StartGameplay()
        {
            towerWeaponModule.StartFeature();
        }

        public override void Release()
        {

        }

    }
}