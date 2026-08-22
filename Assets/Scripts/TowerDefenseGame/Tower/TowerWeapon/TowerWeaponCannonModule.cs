using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.TowerWeapon
{
    public class TowerWeaponCannonModule : TowerWeaponAttackModule
    {
        [SerializeField]
        private Transform projectilePrefab;

        public override void Attack()
        {
            SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            var clone = Instantiate(projectilePrefab, _weaponModule.spawnPoint.position, Quaternion.identity);

            clone.GetComponent<Projectile>().Setup(_weaponModule.AttackTarget, _weaponModule.Damage);
        }


    }
}
