using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Weapon
{
    public readonly struct WeaponStat
    {
        public int level { get; }
        public float damage { get; }
        public float rate { get; }
        public float range { get; }

        public WeaponStat(int level, float damage, float rate, float range)
        {
            this.level = level;
            this.damage = damage;
            this.rate = rate;
            this.range = range;
        }
    }

    public enum WeaponState
    {
        SearchClosestTarget = 0,
        TryAttackWeapon
    }

    public interface IWeaponModuleHost
    {
        IEnumerable<BaseActor> RequestTargetList();
    }

    public class WeaponModule : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private Transform rotateTarget;

        [SerializeField]
        private WeaponAttackModule attackModule;

        private IWeaponModuleHost _host;

        private WeaponStat _stat;

        private Transform _attackTarget;
        private WeaponState _weaponState;

        public int Level => _stat.level;

        public Transform SpawnPoint => spawnPoint;
        public Transform AttackTarget => _attackTarget;

        public float Damage => _stat.damage;
        public float Rate => _stat.rate;
        public float Range => _stat.range;

        public void Init(IWeaponModuleHost host)
        {
            _host = host;

            attackModule.Init(this);
        }

        public void ApplyStat(WeaponStat stat)
        {
            _stat = stat;

            attackModule.ApplyWeaponLevel(stat.level);
        }

        public void StartWeapon()
        {
            ChangeState(WeaponState.SearchClosestTarget);
        }

        public void ChangeState(WeaponState newState)
        {
            StopCoroutine(_weaponState.ToString());

            _weaponState = newState;

            StartCoroutine(_weaponState.ToString());
        }

        private void Update()
        {
            if (_attackTarget != null)
            {
                RotateToTarget();
            }
        }

        private void RotateToTarget()
        {
            float dx = _attackTarget.position.x - rotateTarget.position.x;
            float dy = _attackTarget.position.y - rotateTarget.position.y;

            float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            rotateTarget.rotation = Quaternion.Euler(0, 0, degree);
        }

        private IEnumerator SearchClosestTarget()
        {
            while (true)
            {
                _attackTarget = FindClosestTarget();

                if (_attackTarget != null)
                {
                    ChangeState(WeaponState.TryAttackWeapon);
                }

                yield return null;
            }
        }

        private IEnumerator TryAttackWeapon()
        {
            attackModule.BeginAttack();

            while (true)
            {
                if (!IsPossibleToAttackTarget())
                {
                    attackModule.EndAttack();

                    ChangeState(WeaponState.SearchClosestTarget);
                    break;
                }

                attackModule.Attack();

                yield return new WaitForSeconds(_stat.rate);
            }
        }

        private Transform FindClosestTarget()
        {
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            var targetList = _host.RequestTargetList();

            foreach (var target in targetList)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance <= _stat.range && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }

            }

            return closestTarget;
        }

        private bool IsPossibleToAttackTarget()
        {
            if (_attackTarget == null)
            {
                return false;
            }

            float distance = Vector3.Distance(_attackTarget.position, transform.position);

            if (distance > _stat.range)
            {
                _attackTarget = null;

                return false;
            }

            return true;
        }
    }
}