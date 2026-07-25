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
    private float attackDamage = 1;

    private TowerTemplateSO _towerTemplate;
    private Transform _attackTarget = null;
    private WeaponState _weaponState;
    private int _weaponLevel;

    private ITowerWeaponHandler _weaponHandler;

    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    public void Init(ITowerWeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;

        _towerTemplate = _weaponHandler.TowerTemplate;

        UpdateTowerStat();
    }

    public void Init(BaseActor baseActor, ITowerWeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
        // BaseActor의 모듈로 Get해도 상관없을듯 
        var towerBase = baseActor.GetActorCompoent<TowerBaseModule>("TowerBase");//아직 등록안함 조심
        _towerTemplate = towerBase.TowerTemplate;

        //또는 weaponHandler에 모듈 자체를 Get가능하게 해보던가
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


    public void Upgrade()
    {
        _weaponLevel++;
        UpdateTowerStat();
        
    }
    
    private void UpdateTowerStat()
    {
        attackDamage = _towerTemplate.weapon[_weaponLevel].damage;
        attackRate = _towerTemplate.weapon[_weaponLevel].rate;
        attackRange = _towerTemplate.weapon[_weaponLevel].range;
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