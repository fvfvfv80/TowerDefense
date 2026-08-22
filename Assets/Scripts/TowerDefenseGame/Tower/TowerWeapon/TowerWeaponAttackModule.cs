using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.TowerWeapon
{
    public abstract class TowerWeaponAttackModule : MonoBehaviour
    {
        protected TowerWeaponModule _weaponModule;

        public virtual void Init(TowerWeaponModule weaponModule)
        {
            _weaponModule = weaponModule;
        }

        public virtual void BeginAttack() { }

        public abstract void Attack();

        public virtual void EndAttack() { }

        public virtual void ApplyWeaponLevel(int level) { }
    }
}