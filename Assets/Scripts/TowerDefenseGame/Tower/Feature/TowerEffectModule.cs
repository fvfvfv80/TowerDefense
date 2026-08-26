using Assets.Scripts.TowerDefenseGame.AreaEffect;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Feature
{
    public class TowerEffectModule : MonoBehaviour
    {
        [SerializeField]
        private TowerStatModule towerStatModule;
        [SerializeField]
        private AreaEffectEmitterModule areaEffectEmitterModule;

        public void Init(TowerActor towerActor)
        {
            areaEffectEmitterModule.Init("Enemy");
        }

        public void ApplyLevel(int level)
        {

            var effectData = new EffectData()
            {
                slow = towerStatModule.BaseSpec.slow
            };

            var stat = new EmitterStat(towerStatModule.BaseSpec.range, effectData);

            areaEffectEmitterModule.ApplyStat(stat);
        }
    }
}