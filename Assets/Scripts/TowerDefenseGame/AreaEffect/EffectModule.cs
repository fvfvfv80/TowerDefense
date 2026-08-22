using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.AreaEffect
{

    public struct EffectData
    {
        public float slow;

    }
    public abstract class EffectModule : MonoBehaviour
    {
        protected AreaEffectEmitterModule _emitterModule;

        public virtual void Init(AreaEffectEmitterModule emitterModule)
        {
            _emitterModule = emitterModule;
        }

        public virtual void ApplyStat(EmitterStat stat) { }

        public abstract void ApplyEffect(Collider2D target);

        public abstract void RemoveEffect(Collider2D target);
    }
}