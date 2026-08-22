using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.TowerWeapon
{

    public enum WeaponState { SearchClosestTarget = 0, TryAttackWeapon }




    public class TowerWeaponModule : MonoBehaviour
    {
        [Header("Commons")]
        [SerializeField]
        TowerTemplateSO towerTemplate;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private float attackRate = 0.5f;
        [SerializeField]
        private float attackRange = 2.0f;
        [SerializeField]
        private float attackDamage = 1;

        [Header("TowerAttack")]
        [SerializeField]
        private TowerWeaponAttackModule weaponAttackModule;



        private ITowerWeaponHost _weaponHandler;

        private int _weaponLevel = 0;
        private Transform _attackTarget = null;
        private WeaponState _weaponState;


        public int WeaponLevel => _weaponLevel;
        public Transform SpawnPoint => spawnPoint;
        public Transform AttackTarget => _attackTarget;
        public float Damage => attackDamage;
        public float Rate => attackRate;
        public float Range => attackRange;



        public void Init(ITowerWeaponHost weaponHandler)
        {
            _weaponHandler = weaponHandler;

            weaponAttackModule.Init(this);
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



        public void SetWeaponLevel(int level)
        {
            _weaponLevel = level;
            attackDamage = towerTemplate.weapon[_weaponLevel].damage;
            attackRate = towerTemplate.weapon[_weaponLevel].rate;
            attackRange = towerTemplate.weapon[_weaponLevel].range;

            weaponAttackModule.ApplyWeaponLevel(_weaponLevel);
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
                    ChangeState(WeaponState.TryAttackWeapon);

                }
                    

                yield return null;

            }
        }

        private IEnumerator TryAttackWeapon()
        {
            weaponAttackModule.BeginAttack();

            while (true)
            {
                if (!IsPossibleToAttackTarget())
                {
                    weaponAttackModule.EndAttack();
                    ChangeState(WeaponState.SearchClosestTarget);
                    break;
                }

                weaponAttackModule.Attack();

                yield return new WaitForSeconds(attackRate);
            }
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
