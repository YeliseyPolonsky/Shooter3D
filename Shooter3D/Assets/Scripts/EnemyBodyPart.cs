using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    [SerializeField] private float _damageMultiplier = 1f;
    
    private Rigidbody _rb;
    private EnemyBodyPartManager _manager;
    
    public void Init(EnemyBodyPartManager manager)
    {
        _manager = manager;
        _rb = GetComponent<Rigidbody>();
    }

    public void MakeKinematic()
    {
        _rb.isKinematic = true;
    }

    public void MakePhysical()
    {
        _rb.isKinematic = false;
    }

    public void Hit(float damage, EnemyBodyPart enemyBodyPart, Vector3 direction)
    {
        _manager.Hit(damage * _damageMultiplier, enemyBodyPart, direction);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rb.velocity = velocity;
    }
}
