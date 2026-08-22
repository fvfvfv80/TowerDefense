using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public abstract class TowerFeatureModule : MonoBehaviour
    {
        public virtual void Init(TowerActor towerActor) { }

        public virtual void ApplyLevel(int level) { }

        public virtual void StartFeature() { }

        public virtual void Release() { }
    }
}