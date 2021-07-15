using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 7f;
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float timeToWait = 1f;
    [SerializeField] private float chaseToTimer = 3f;
    [SerializeField] private Transform ModelEnemyTransform;
    [SerializeField] private Animator anim;
    [SerializeField] private bool animActive;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    private float _waitTime;
    private float _chaseTime;
    private float _walkSpeed;

    private bool _isWait = false;
    private bool _isChasingPlayer;
    private bool _colliderWithPlayer;

    private Vector2 _pointVector;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;

    public bool IsFacingRight { get; private set; } = true;

    public void StartChasingPlayer() 
    {
        _isChasingPlayer = true;
        _walkSpeed = chaseSpeed;
        _chaseTime = chaseToTimer;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = ModelEnemyTransform.position;
        _rightBoundaryPosition = (Vector2)transform.position + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = chaseToTimer;
        _walkSpeed = patrolSpeed;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    private void Update()
    {
        if (_isChasingPlayer)
            StartChaseToTimer();
        if (ShouldWait()) 
        {
            _isWait = true;
        }
        if (_isWait && !_isChasingPlayer) 
        {
            StartTimeToWait();
        }
    }

    private void FixedUpdate()
    {
        if (_isChasingPlayer && _colliderWithPlayer) 
        {
            return;
        }
        _pointVector = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChasingPlayer) 
        {
            ChasePlayer();
        }
        if (!_isWait && !_isChasingPlayer)
        {
            Patrol();
        }
    }
    private void Patrol() 
    {
        if (!IsFacingRight)
            _pointVector *= -1;
        _rb.MovePosition((Vector2)transform.position + _pointVector);
    }
    private void ChasePlayer() 
    {
        float distance = DistanceToPLayer();
        if (distance < 0) 
        {
            _pointVector *= -1;
        }
        if (distance > 0.2f && !IsFacingRight)
         {
             Flip();
         } else if (distance < 0.2f && IsFacingRight) 
         {
             Flip();
         }
        if(animActive)
            anim.SetBool("Run",true);
        _rb.MovePosition((Vector2)transform.position + _pointVector);
    }
    private bool ShouldWait() 
    {
        bool isRightBoundaryPosition = IsFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isLeftBoundaryPosition = !IsFacingRight && transform.position.x <= _leftBoundaryPosition.x;

        return isRightBoundaryPosition || isLeftBoundaryPosition;
    }

    private float DistanceToPLayer() 
    {
        return _playerTransform.position.x - transform.position.x;
    }
    private void StartTimeToWait()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0)
        {
            _isWait = false;
            _waitTime = timeToWait;
            Flip();
        }
    }
    private void StartChaseToTimer() 
    {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime < 0) 
        {
            _isChasingPlayer = false;
            _walkSpeed = patrolSpeed;
            _chaseTime = chaseToTimer;
        }
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 playerScale = ModelEnemyTransform.localScale;
        playerScale.x *= -1;
        ModelEnemyTransform.localScale = playerScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition,_rightBoundaryPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();

        if (_playerController != null) 
        {
            _colliderWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();

        if (_playerController != null)
        {
            _colliderWithPlayer = false;
        }
    }
}
