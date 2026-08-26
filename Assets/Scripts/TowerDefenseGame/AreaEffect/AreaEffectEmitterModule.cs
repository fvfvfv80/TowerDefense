using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.AreaEffect
{
    public readonly struct EmitterStat
    {
        public readonly float radius;
        public readonly EffectData effectData;

        public EmitterStat(float radius, EffectData effectData)
        {
            this.radius = radius;
            this.effectData = effectData;
        }
    }

    public class AreaEffectEmitterModule : MonoBehaviour
    {
        [SerializeField]
        private CircleCollider2D effectArea;
        [SerializeField] 
        private string targetTag = "Enemy";
        [SerializeField] 
        private EffectModule effectModule;

        public void Init(string targetTag)
        {
            effectModule.Init(this);
            this.targetTag = targetTag;
        }

        public void ApplyStat(EmitterStat stat)
        {
            effectArea.radius = stat.radius;
            effectModule.ApplyStat(stat);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(targetTag)) return;

            effectModule.ApplyEffect(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag(targetTag)) return;

            effectModule.RemoveEffect(collision);
        }
    }
}