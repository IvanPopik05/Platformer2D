using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject pauseCanvas;
    bool restartActive = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RestartHandler() 
    {
        animator.SetTrigger("HideWindow");
        restartActive = true;
        Time.timeScale = 1f;
    }

    public void SceneRestartrMethod() 
    {
        if (!pauseCanvas.activeInHierarchy || restartActive) 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        restartActive = false;
        gameObject.SetActive(false);
    }
    public void ExitHandler() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
