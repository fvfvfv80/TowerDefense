using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D _movement2D;
    private Transform _target;
    private int _damage;


    public void Init()
    {
        _movement2D = GetComponent<Movement2D>();
    }

    public void Setup(Transform target, int damage)
    {
        _movement2D = GetComponent<Movement2D>();

        _target = target;

        _damage = damage;

    }

    private void Update()
    {
        if(_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        if (collision.transform != _target)
            return;

        collision.GetComponent<EnemyHPModule>().TakeDamage(_damage);
        Destroy(gameObject);
    }

}