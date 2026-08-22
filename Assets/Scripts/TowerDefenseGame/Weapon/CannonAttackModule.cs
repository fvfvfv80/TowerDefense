using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Weapon
{
    public class CannonAttackModule : WeaponAttackModule
    {
        [SerializeField]
        private Transform projectilePrefab;

        public override void Attack()
        {
            SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            var clone = Instantiate(projectilePrefab, _weaponModule.SpawnPoint.position, Quaternion.identity);

            clone.GetComponent<Projectile>().Setup(_weaponModule.AttackTarget, _weaponModule.Damage);

        }
    }
}