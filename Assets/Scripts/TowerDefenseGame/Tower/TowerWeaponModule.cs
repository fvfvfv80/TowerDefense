using Assets.Scripts.TowerDefenseGame.Tower;
using System.Collections;
using UnityEngine;

public enum WeaponState { SearchClosestTarget = 0, AttackToTarget }

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
    private int attackDamage = 1;


    private Transform _attackTarget = null;
    private WeaponState _weaponState;
    private int _level;

    private ITowerWeaponHandler _weaponHandler;

    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;
    public int Level => _level + 1;

    public void Init(ITowerWeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    public void Setup()
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