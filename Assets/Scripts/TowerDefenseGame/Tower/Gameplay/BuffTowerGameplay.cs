using Assets.Scripts.TowerDefenseGame.Tower.Feature;
using Assets.Scripts.TowerDefenseGame.Tower.GamePlay;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Gameplay
{
    public class BuffTowerGameplay : TowerGameplay
    {
        [SerializeField]
        private TowerDamageSupportModule towerSupportModule;

        public override void Init(TowerActor towerActor)
        {
            towerSupportModule.Init(towerActor);
        }

        public override void Setup()
        {
            towerSupportModule.Setup();
        }

        public override void StartGameplay()
        {
            towerSupportModule.StartSupport();
        }

        public override void ApplyLevel(int level)
        {
            towerSupportModule.ApplyLevel(level);
        }

        public override void HandleCommand(TowerGameplayCommand command)
        {
            switch(command.type)
            {
                case TowerGameplayCommandType.ApplyBuff:
                    towerSupportModule.TryApplyBuff(command.buffTargetTower);
                    break;
            }
        }

        public override void Release()
        {
            towerSupportModule.EndSupport();
        }




    }
}