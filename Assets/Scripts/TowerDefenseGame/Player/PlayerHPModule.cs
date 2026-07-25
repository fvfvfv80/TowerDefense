using System;
using UnityEngine;

public class PlayerHPModule : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20;

    private float _currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => _currentHP;

    public Action OnTakeDamage;

    private void Awake()
    {
        _currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;

        OnTakeDamage?.Invoke();

        if (_currentHP <= 0)
        {

        }
    }
}
