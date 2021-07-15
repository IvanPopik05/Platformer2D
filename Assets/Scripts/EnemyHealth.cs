using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthSlider;

    private float _health;
    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }
    public void Hit(float damage) 
    {
        _health -= damage;
        InitHealth();
        animator.SetTrigger("Hit");
        if (_health <= 0) 
        {
            Die();
        }
    }

    private void InitHealth() 
    {
        healthSlider.value = _health / totalHealth;
    }

    private void Die() 
    {
        gameObject.SetActive(false);
    }


}
