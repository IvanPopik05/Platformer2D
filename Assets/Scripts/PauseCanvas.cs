using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void ContinueHandler() 
    {
        Time.timeScale = 1f;
        animator.SetTrigger("HideWindow");
    }
    public void PauseHandler() 
    {
        animator.SetTrigger("ShowWindow");
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
