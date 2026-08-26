using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Weapon
{
    public class LaserAttackModule : WeaponAttackModule
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private Transform hitEffect;

        [SerializeField]
        private LayerMask targetLayer;


        public override void Init(WeaponModule weaponModule)
        {
            base.Init(weaponModule);

            DisableLaser();

            ApplyWeaponLevel(0);
        }

        public override void BeginAttack()
        {
            UpdateLaserPosition();
            EnableLaser();
        }

        public override void Attack()
        {
            UpdateLaserPosition();
            ApplyLaserDamage();
        }

        public override void EndAttack()
        {
            DisableLaser();
        }


        public override void ApplyWeaponLevel(int level)
        {
            lineRenderer.startWidth = 0.05f + level * 0.05f;

            lineRenderer.endWidth = 0.05f;
        }


        private void EnableLaser()
        {
            lineRenderer.gameObject.SetActive(true);
            hitEffect.gameObject.SetActive(true);
        }

        private void DisableLaser()
        {
            lineRenderer.gameObject.SetActive(false);
            hitEffect.gameObject.SetActive(false);
        }


        private void UpdateLaserPosition()
        {
            Vector3 direction = (_weaponModule.AttackTarget.position - _weaponModule.SpawnPoint.position).normalized;

            var hits = Physics2D.RaycastAll(_weaponModule.SpawnPoint.position, direction, _weaponModule.Range, targetLayer);

            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].transform != _weaponModule.AttackTarget)
                {
                    continue;
                }

                lineRenderer.SetPosition(0, _weaponModule.SpawnPoint.position);

                lineRenderer.SetPosition(1, new Vector3(hits[i].point.x, hits[i].point.y, 0) + Vector3.back);

                hitEffect.position = hits[i].point;

                break;
            }
        }

        private void ApplyLaserDamage()
        {
            _weaponModule.AttackTarget.GetComponent<IHPModule>().TakeDamage(_weaponModule.Damage * Time.deltaTime);

        }
    }
}