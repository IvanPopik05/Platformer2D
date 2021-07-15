using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerAttack _playerAttack;
    private void Start()
    {
        GameObject findObject = GameObject.FindGameObjectWithTag("Player");
        _playerAttack = findObject.GetComponent<PlayerAttack>();
        _playerController = findObject.GetComponent<PlayerController>();
    }
    public void Jump() 
    {
        _playerController.Jump();
    }

    public void Attack() 
    {
        _playerAttack.Attack();
    }
    public void Interact() 
    {
        _playerController.Interact();
    }
}
