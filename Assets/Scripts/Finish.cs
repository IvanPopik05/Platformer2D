using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject finishPromptImage;

    private Animator animator;
    public bool IsActiveLevel { get; set; } = false;
    public void Activate()
    {
        IsActiveLevel = true;
        finishPromptImage.SetActive(false);
    }
    private void Start()
    {
        animator = levelCompleteCanvas.GetComponent<Animator>();
    }
    public void FinishLevel() 
    {
        if (IsActiveLevel)
        {
            animator.SetTrigger("ShowWindow");
            levelCompleteCanvas.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        else 
        {
            finishPromptImage.SetActive(true);
        }
    }
}
