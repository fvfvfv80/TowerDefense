using Assets.Scripts.TowerDefenseGame.Tower;
using Assets.Scripts.TowerDefenseGame.Tower.TowerWeapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Legacy
{
    public class TowerWeaponModule : MonoBehaviour
    {

        [Header("Commons")]
        [SerializeField]
        private WeaponType weaponType;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private float attackRate = 0.5f;
        [SerializeField]
        private float attackRange = 2.0f;
        [SerializeField]
        private float attackDamage = 1;


        [Header("Cannon")]
        [SerializeField]
        private Transform projectilePrefab;

        [Header("Laser")]
        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private Transform hitEffect;
        [SerializeField]
        private LayerMask targetLayer;



        private int _weaponLevel = 1; //이게 필요하게되면 그냥 템플릿 넘겨도될듯
        private Transform _attackTarget = null;
        private WeaponState _weaponState;

        private ITowerWeaponHost _weaponHandler;

        public float Damage => attackDamage;
        public float Rate => attackRate;
        public float Range => attackRange;

        public void Init(ITowerWeaponHost weaponHandler)
        {
            _weaponHandler = weaponHandler;

            if (weaponType == WeaponType.Laser)
                DisableLaser();
        }


        public void StartTower()
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


        public void SetWeaponStat(TowerWeaponStat weaponStat)
        {
            attackDamage = weaponStat.Damage;
            attackRate = weaponStat.Rate;
            attackRange = weaponStat.Range;
        }


        public void SetWeaponLevel(int level)
        {
            _weaponLevel = level;
            lineRenderer.startWidth = 0.05f + _weaponLevel * 0.05f;
            lineRenderer.endWidth = 0.05f;
        }


        private void RotateToTarget()
        {
            float dx = _attackTarget.position.x - transform.position.x;
            float dy = _attackTarget.position.y - transform.position.y;

            float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, degree);

        }

        private IEnumerator SearchClosestTarget()
        {

            while (true)
            {
                _attackTarget = FindClossestTarget();

                if (_attackTarget != null)
                {
                    if (weaponType == WeaponType.Cannon)
                        ChangeState(WeaponState.TryAttackCannon);
                    else if (weaponType == WeaponType.Laser)
                        ChangeState(WeaponState.TryAttackLaser);
                }


                yield return null;

            }
        }

        private IEnumerator TryAttackCannon()
        {
            while (true)
            {
                if (!IsPossibleToAttackTarget())
                {
                    ChangeState(WeaponState.SearchClosestTarget);
                    break;
                }

                yield return new WaitForSeconds(attackRate);

                SpawnProjectile();

            }


        }

        private IEnumerator TryAttackLaser()
        {
            EnableLaser();

            while (true)
            {
                if (!IsPossibleToAttackTarget())
                {
                    DisableLaser();
                    ChangeState(WeaponState.SearchClosestTarget);
                    break;
                }

                SpawnLaser();

                yield return null;
            }
        }

        private void SpawnLaser()
        {
            Vector3 direction = (_attackTarget.position - spawnPoint.position).normalized;
            var hit = Physics2D.RaycastAll(spawnPoint.position, direction, attackRange, targetLayer);

            for (int i = 0; i < hit.Length; ++i)
            {
                if (hit[i].transform == _attackTarget)
                {
                    lineRenderer.SetPosition(0, spawnPoint.position);
                    lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);

                    hitEffect.position = hit[i].point;

                    _attackTarget.GetComponent<IHPModule>().TakeDamage(attackDamage * Time.deltaTime);
                }
            }


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




        private void SpawnProjectile()
        {
            var clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            clone.GetComponent<Projectile>().Setup(_attackTarget, attackDamage);
        }

        private Transform FindClossestTarget()
        {
            float closestDistSqr = Mathf.Infinity;

            var targetList = _weaponHandler.RequestTowerTargetList();
            foreach (var target in targetList)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    _attackTarget = target.transform;
                }
            }

            return _attackTarget;
        }

        private bool IsPossibleToAttackTarget()
        {
            if (_attackTarget == null)
            {
                return false;
            }

            float distance = Vector3.Distance(_attackTarget.position, transform.position);

            if (distance > attackRange)
            {
                _attackTarget = null;

                return false;
            }

            return true;
        }

    }
}
   