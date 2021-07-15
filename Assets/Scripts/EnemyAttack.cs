using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damageTouch = 15;
    [SerializeField] private float attackToWait;

    private float _attackWait;
    private bool isAttack = true;

    private void Start()
    {
        _attackWait = attackToWait;
    }
    private void Update()
    {
        if (!isAttack) 
        {
            _attackWait -= Time.deltaTime;
            if (_attackWait <= 0) 
            {
                isAttack = true;
                _attackWait = attackToWait;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (_playerHealth != null && isAttack) 
        {
            Debug.Log("Damage Player");
            _playerHealth.Hit(damageTouch);
            isAttack = false;
        }
    }
}
