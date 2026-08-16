using Assets.Scripts.TowerDefenseGame.Tower;
using System.Collections;
using UnityEngine;

public enum WeaponType { Cannon = 0, }

public enum WeaponState { SearchClosestTarget = 0, AttackToTarget }

public struct TowerWeaponStat
{
    public float Damage;
    public float Rate;
    public float Range;
}

public class TowerWeaponModule : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform projectilePrefab;

    [SerializeField]
    private float attackRate = 0.5f; 
    [SerializeField]
    private float attackRange = 2.0f;
    [SerializeField]
    private float attackDamage = 1;

    private Transform _attackTarget = null;
    private WeaponState _weaponState;

    private ITowerWeaponHost _weaponHandler;

    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    public void Init(ITowerWeaponHost weaponHandler)
    {
        _weaponHandler = weaponHandler;
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


    
    private void RotateToTarget()
    {
        float dx = _attackTarget.position.x - transform.position.x;
        float dy = _attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);

    }

    private IEnumerator SearchClosestTarget()
    {
        float closestDistSqr = Mathf.Infinity;

        while (true)
        {
            var targetList = _weaponHandler.GetTargetList();
            foreach(var target in targetList)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    _attackTarget = target.transform;
                }
            }

            if (_attackTarget != null)
                ChangeState(WeaponState.AttackToTarget);

            yield return null;

        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            if (_attackTarget == null)
            {
                ChangeState(WeaponState.SearchClosestTarget);
                break;
            }

            float distance = Vector3.Distance(_attackTarget.position, transform.position);

            if (distance > attackRange)
            {
                _attackTarget = null;
                ChangeState(WeaponState.SearchClosestTarget);
                break;
            }

            yield return new WaitForSeconds(attackRate);

            SpawnProjectile();

        }


    }

    private void SpawnProjectile()
    {
        var clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        clone.GetComponent<Projectile>().Setup(_attackTarget,1);
    }


}