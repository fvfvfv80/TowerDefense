using Assets.Scripts.TowerDefenseGame.Tower.Feature;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Gameplay
{
    public class EffectTowerGameplay : TowerGameplay
    {
        [SerializeField]
        private TowerEffectModule towerEffectModule;

        public override void Init(TowerActor towerActor)
        {
            towerEffectModule.Init(towerActor);
        }

        public override void Setup()
        {
            towerEffectModule.ApplyLevel(0);
        }

        public override void StartGameplay()
        {

        }

        public override void ApplyLevel(int level)
        {
            towerEffectModule.ApplyLevel(level);
        }

        public override void UpdateStat() { }

        public override void Release()
        {

        }

    }
}