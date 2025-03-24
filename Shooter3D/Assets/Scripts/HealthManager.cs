using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private EnemyBodyPartManager _enemyBodyPartManager;
    [SerializeField] private float _maxHealth = 100f;
    private float _health;
    
    private void Start()
    {
        _health = _maxHealth;
    }

    public void ApplyDamage(float damage, EnemyBodyPart enemyBodyPart, Vector3 direction)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die(enemyBodyPart, direction);
        }
    }

    private void Die(EnemyBodyPart enemyBodyPart, Vector3 direction)
    {
        _enemyBodyPartManager.MakePhysics(enemyBodyPart, direction);
    }
}
