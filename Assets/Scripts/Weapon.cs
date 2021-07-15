using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damageWeapon;

    private AudioSource _audioSwordSwing;
    private PlayerAttack _playerAttack;
    private void Start()
    {
        _playerAttack = transform.root.GetComponent<PlayerAttack>();
        _audioSwordSwing = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth _enemyHealth = collision.GetComponent<EnemyHealth>();

        if (_enemyHealth != null && _playerAttack.IsAttack) 
        {
            Debug.Log("Attack");
            _audioSwordSwing.Play();
            _enemyHealth.Hit(damageWeapon);
        }
    }
}
