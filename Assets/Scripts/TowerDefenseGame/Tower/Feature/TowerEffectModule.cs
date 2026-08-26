using Assets.Scripts.TowerDefenseGame.AreaEffect;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Feature
{
    public class TowerEffectModule : TowerFeatureModule
    {
        [SerializeField] private TowerTemplateSO towerTemplate;
        [SerializeField] private AreaEffectEmitterModule areaEffectEmitterModule;

        public override void Init(TowerActor towerActor)
        {
            areaEffectEmitterModule.Init("Enemy");
        }

        public override void ApplyLevel(int level)
        {
            var levelData = towerTemplate.weapon[level];

            var effectData = new EffectData()
            {
                slow = levelData.slow
            };


            var stat = new EmitterStat(levelData.range, effectData);

            areaEffectEmitterModule.ApplyStat(stat);
        }
    }
}