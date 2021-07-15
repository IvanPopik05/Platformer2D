using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private Animator animator;
    [SerializeField] private float speedX;
    [SerializeField] private bool isJump = false;
    [SerializeField]private bool isGround = false;

    private Finish _finishBuilding;
    private bool _isFacingRight = true;
    const float _speedMultiply = 20f;
    private Rigidbody2D _rb;
    float _horizontal;
    private bool _isFinishBuilding;
    private bool _isLeverArm;
    private LeverArm _leverArm;
    private FixedJoystick _fixedJoystick;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finishBuilding = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _fixedJoystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>();
        _leverArm = FindObjectOfType<LeverArm>();
    }
    private void Update()
    {
        //_horizontal = Input.GetAxis("Horizontal");
        _horizontal = _fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }
    public void Jump() 
    {
        if (!isGround) return;
        jumpAudio.Play();
        isJump = true;
    }
    public void Interact() 
    {
        if (_isFinishBuilding)
            _finishBuilding.FinishLevel();
        if (_isLeverArm)
            _leverArm.ActivateLeverArm();
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * _speedMultiply * Time.fixedDeltaTime, _rb.velocity.y);

        if (_horizontal < 0 && !_isFacingRight)
        {
            Flip();
            //transform.localScale = new Vector3(1,transform.localScale.y,transform.localScale.z);
            //isFacingRight = true;
        }
        else if (_horizontal > 0 && _isFacingRight)
        {
            Flip();
            //transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);
            //isFacingRight = false;
            
        }

        if (isJump) 
        {
            _rb.AddForce(new Vector2(0, 650));
            isGround = false;
            isJump = false;
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeverArm leverArmDemo = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish")) 
        {
            _isFinishBuilding = true;
        }
        if (leverArmDemo != null) 
        {
            _isLeverArm = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LeverArm leverArmDemo = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish"))
        {
            _isFinishBuilding = false;
        }
        if (leverArmDemo != null)
        {
            _isLeverArm = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}

