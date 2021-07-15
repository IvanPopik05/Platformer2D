using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private AudioSource hurtAudio;

    private Animator animatorScreen;
    private List<GameObject> children = new List<GameObject>();
    private int count;

    private void Start()
    {
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            children.Add(parentObject.transform.GetChild(i).gameObject);
        }
        count = 0;
        animatorScreen = _gameOverScreen.GetComponent<Animator>();
    }
    public void Hit(float damage)
    {
        hurtAudio.Play();
        health -= damage;
        children[count].SetActive(false);
        if (count > children.Count - 1)
            throw new ArgumentException("Большой счётчик");
        count += 1;
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        WindowShow();
    }

    private void WindowShow() 
    {
        animatorScreen.SetTrigger("ShowWindow");
        _gameOverScreen.gameObject.SetActive(true);
    }


}
