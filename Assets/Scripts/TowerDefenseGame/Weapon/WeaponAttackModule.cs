using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Weapon
{
    public abstract class WeaponAttackModule : MonoBehaviour
    {
        protected WeaponModule _weaponModule;

        public virtual void Init(WeaponModule weaponModule)
        {
            _weaponModule = weaponModule;
        }

        public virtual void BeginAttack() { }

        public abstract void Attack();

        public virtual void EndAttack() { }

        public virtual void ApplyWeaponLevel(int level) { }
    }
}