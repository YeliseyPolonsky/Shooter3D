using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBodyPartManager : MonoBehaviour
{
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private Animator _animator;
    private EnemyBodyPart[] _bodyParts;

    private void Start()
    {
        _bodyParts = GetComponentsInChildren<EnemyBodyPart>();
        
        foreach (var bodyPart in _bodyParts)
        {
            bodyPart.Init(this);
        }
        
        MakeKinematic();
    }

    private void MakeKinematic()
    {
        foreach (var bodyPart in _bodyParts)
        {
            bodyPart.MakeKinematic();
        }
    }
    
    [ContextMenu("MakePhysics")]
    public void MakePhysics(EnemyBodyPart enemyBodyPart, Vector3 direction)
    {   
        foreach (var bodyPart in _bodyParts)
        {
            bodyPart.MakePhysical();
        }
        
        enemyBodyPart.SetVelocity(direction * 40f);

        _animator.enabled = false;
    }

    public void Hit(float damage, EnemyBodyPart enemyBodyPart, Vector3 direction)
    {
        _healthManager.ApplyDamage(damage, enemyBodyPart, direction);
    }
}