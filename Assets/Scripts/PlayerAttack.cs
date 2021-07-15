using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttack;
    [SerializeField] private Animator animator;
    public bool IsAttack { get => isAttack; private set => isAttack = value; }

    public void FinishAttack() 
    {
        isAttack = false;
    }
    //private void Update()
    //{
        //if (Input.GetMouseButtonDown(0)) 
        //{
        //    Attack();
        //}
    //}

    public void Attack() 
    {
        isAttack = true;
        animator.SetTrigger("Attack");
    }
}
